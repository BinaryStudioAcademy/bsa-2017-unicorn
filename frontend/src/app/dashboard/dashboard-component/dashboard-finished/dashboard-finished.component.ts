import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';
import { NotificationService } from "../../../services/notifications/notification.service";

import { SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import { ReviewModal } from '../../../review/review-modal/review-modal.component';

import { Subscription } from 'rxjs/Subscription';
import { MapModel } from "../../../models/map.model";
export interface IDeclineConfirm {
  id: number;
}
@Component({
  selector: 'app-dashboard-finished',
  templateUrl: './dashboard-finished.component.html',
  styleUrls: ['./dashboard-finished.component.sass']
})
export class DashboardFinishedComponent implements OnInit, OnDestroy {
  @ViewChild('mapModal')
  public modalTemplate:ModalTemplate<IDeclineConfirm, void, void>
  currModal: SuiActiveModal<IDeclineConfirm, {}, void>;
  books: BookCard[];
  sub: Subscription;
  map: MapModel;
  constructor(
    private dashboardService: DashboardService,
    private dashMessaging: DashMessagingService,
    private notificationService: NotificationService,
    public modalService: SuiModalService,
    private datePipe: DatePipe
  ) { }
  
  ngOnInit() {
    this.loadData();
    this.sub = this.dashMessaging.progressEvent$.subscribe(() => {
      this.loadData();
    });
    this.notificationService.listen<any>("RefreshOrders", () => this.loadData());
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loadData() {
    this.dashboardService.getFinishedBooks().then(resp => {
      this.books = resp.sort((b1, b2) => {
        let f = new Date(b1.Date).getTime();
        let s = new Date(b2.Date).getTime();
        return s - f;
      });
      console.log(this.books);
    });
  }

  getBookById(id: number): BookCard {
    return this.books.filter(b => b.Id == id)[0];
  }

  openModal(id: number) {
    let book = this.getBookById(id);
    this.modalService.open(new ReviewModal(book.Review));
  }

  isRated(id: number): boolean {
    let book = this.getBookById(id);
    return book.Status == BookStatus.Confirmed;
  }

  getEndDate(book: BookCard): string {
    let date = this.datePipe.transform(book.Date, 'dd/MM/yyyy');
    let endDate = this.datePipe.transform(book.EndDate, 'dd/MM/yyyy');
    if (date == endDate) {
      return '';
    }
    return ` - ${endDate}`;
  }

  openMap(id:number)
  { 
    this.map = {
      center: {lat: this.books.filter(b => b.Id == id)[0].Location.Latitude, lng: this.books[0].Location.Longitude},
      zoom: 18,    
      title: this.books.filter(b => b.Id == id)[0].Customer,
      label: this.books.filter(b => b.Id == id)[0].Customer,
      markerPos: {lat: this.books.filter(b => b.Id == id)[0].Location.Latitude, lng: this.books.filter(b => b.Id == id)[0].Location.Longitude}    
    };  
   const config = new TemplateModalConfig<IDeclineConfirm, void, void>(this.modalTemplate);
   config.isInverted = true;
   config.size = ModalSize.Tiny;
   this.currModal = this.modalService.open(config);
  }
}
