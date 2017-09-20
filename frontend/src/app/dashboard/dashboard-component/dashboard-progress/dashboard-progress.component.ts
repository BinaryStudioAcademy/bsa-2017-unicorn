import { Component, OnInit, OnDestroy, ViewChild  } from '@angular/core';
import { DatePipe } from '@angular/common';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';
import { NotificationService } from "../../../services/notifications/notification.service";

import {ToastsManager, Toast} from 'ng2-toastr';
import {ToastOptions} from 'ng2-toastr';
import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';

import { Subscription } from 'rxjs/Subscription';
import { MapModel } from "../../../models/map.model";
import { DashboardEventsService } from "../../../services/events/dashboard-events.service";
export interface IDeclineConfirm {
  id: number;
}
@Component({
  selector: 'app-dashboard-progress',
  templateUrl: './dashboard-progress.component.html',
  styleUrls: ['./dashboard-progress.component.sass']
})
export class DashboardProgressComponent implements OnInit, OnDestroy {
  @ViewChild('mapModal')
  public modalTemplate:ModalTemplate<IDeclineConfirm, void, void>
  currModal: SuiActiveModal<IDeclineConfirm, {}, void>;
  books: BookCard[];
  loads: {[bookId: number]: boolean} = {};
  sub: Subscription;
  map: MapModel;

  constructor(
    private dashboardService: DashboardService,
    private dashMessaging: DashMessagingService,
    private notificationService: NotificationService,
    private toastr: ToastsManager,
    private modalService: SuiModalService,
    private datePipe: DatePipe,
    private dashboardEventsService: DashboardEventsService
  ) { }

  ngOnInit() {
    this.loadData();
    this.sub = this.dashMessaging.pendingEvent$.subscribe(() => {
      this.loadData();
    });
    this.notificationService.listen<any>("RefreshOrders", () => this.loadData());
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loadData() {
    this.dashboardService.getAcceptedBooks().then(resp => {
      this.books = resp.sort((b1, b2) => {
        let f = new Date(b1.Date).getTime();
        let s = new Date(b2.Date).getTime();
        return s - f;
      });
      console.log(this.books);
    });
  }

  finish(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Finished;
    this.loads[book.Id] = true;
    this.dashboardService.update(book).then(resp => {
      this.dashboardEventsService.changeStatusToFinished();
      this.books.splice(this.books.findIndex(b => b.Id === id), 1);
      this.loads[book.Id] = false;
      this.dashMessaging.changeProgress();
      this.toastr.success('Finished task');
    })
    .catch(err => {
      this.loads[book.Id] = false;      
      this.toastr.error('Cannot finish task');
    });
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
