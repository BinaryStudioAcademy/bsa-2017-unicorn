import { Component, OnInit, ViewChild } from '@angular/core';
import { DatePipe } from '@angular/common'; 

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';
import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { NotificationService } from "../../../services/notifications/notification.service";

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';

import {ToastsManager, Toast} from 'ng2-toastr';
import {ToastOptions} from 'ng2-toastr';
import { MapModel } from "../../../models/map.model";
import { DashboardEventsService } from "../../../services/events/dashboard-events.service";
import { Subscription } from "rxjs/Subscription";

export interface IDeclineConfirm {
  id: number;
}
@Component({
  selector: 'app-dashboard-pendings',
  templateUrl: './dashboard-pendings.component.html',
  styleUrls: ['./dashboard-pendings.component.sass']
})
export class DashboardPendingsComponent implements OnInit {

  
  @ViewChild('mapModal')
  public modalTemplate:ModalTemplate<IDeclineConfirm, void, void>

  @ViewChild('declineModal')
  public declineTemplate:ModalTemplate<IDeclineConfirm, void, void>

  currModal: SuiActiveModal<IDeclineConfirm, {}, void>;

  books: BookCard[];
  changeStatusToFinished: Subscription;

  aloads: {[bookId: number]: boolean} = {};
  dloads: {[bookId: number]: boolean} = {};

  reason: string;
  loader: boolean;
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
    this.notificationService.listen<any>("RefreshOrders", () => this.loadData());
    this.changeStatusToFinished = this.dashboardEventsService.changeStatusToFinishedEvent$.subscribe(() => {
      this.books = undefined;
      this.loadData();
    })
  }

  loadData() {
    this.dashboardService.getPendingBooks().then(resp => {
      this.books = resp.sort((b1, b2) => {
        let f = new Date(b1.Date).getTime();
        let s = new Date(b2.Date).getTime();
        return s - f;
      });           
    });
  }

  accept(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Accepted;
    this.aloads[book.Id] = true;
    this.dashboardService.update(book)
    .then(resp => {
      this.dashboardService.getPendingBooks().then(resp => {
        this.books = resp;
        this.dashMessaging.changePending();      
        this.aloads[book.Id] = false;
        this.toastr.success('Accepted task');
      });
    })
    .catch(err => {
      this.aloads[book.Id] = false;      
      this.toastr.error('Ops. Cannot accept task');
    });
  }

  decline(id: number) {
    this.reason = '';
    const config = new TemplateModalConfig<IDeclineConfirm, void, void>(this.declineTemplate);
    config.context = {id: id};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }

  declineConfirm(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Declined;
    book.DeclinedReason = this.reason;
    this.loader = true;
    this.dashboardService.update(book).then(resp => {
      this.books.splice(this.books.findIndex(b => b.Id === id), 1);
      this.loader = false;
      this.currModal.deny(undefined);
      this.toastr.success('Declined task');
    }).catch(err => {
      this.loader = false;
      this.currModal.deny(undefined);
      this.toastr.error('Ops. Cannot decline task');
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

 ngOnDestroy() {
  this.changeStatusToFinished.unsubscribe();  
}
}
