import { Component, OnInit, Input, ViewChild, OnDestroy } from '@angular/core';
import { NgModel } from '@angular/forms';
import { User } from '../../models/user';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../review/review-modal/review-modal.component';

import { CustomerbookService } from '../../services/customerbook.service';
import { ReviewService } from '../../services/review.service';
import { NotificationService } from "../../services/notifications/notification.service";
import { TaskMessagingService } from '../../services/task-messaging.service';

import { CustomerBook, BookStatus } from '../../models/book/book.model';

import { ShortReview } from '../../models/short-review';

import { MapModel } from "../../models/map.model";

import { Subscription } from 'rxjs/Subscription';

export interface IMapModal {
  id: number;
}

@Component({
  selector: 'app-user-history',
  templateUrl: './user-history.component.html',
  styleUrls: ['./user-history.component.sass']
})
export class UserHistoryComponent implements OnInit, OnDestroy {

  @Input() user: User;
  
  @ViewChild('mapModal')
  public modalTemplate:ModalTemplate<IMapModal, void, void>

  books: CustomerBook[];
  
  sub: Subscription;

  map: MapModel;   
  currModal: SuiActiveModal<IMapModal, {}, void>;  

    constructor(
      private bookService: CustomerbookService,
      private modalService: SuiModalService,
      private reviewService: ReviewService,
      private notificationService: NotificationService,
      private taskMessaging: TaskMessagingService
    ) { }
  
    ngOnInit() {
      this.loadData();
      this.notificationService.listen<any>("RefreshOrders", () => this.loadData());
      this.sub = this.taskMessaging.taskFinishedEvent.subscribe(() => {
        this.loadData();
      });
    }

    ngOnDestroy() {
      this.sub.unsubscribe();
    }
  
    loadData() {
      this.bookService.getCustomerBooks(this.user.Id)
      .then(resp => {
        this.books = resp.filter(b => b.Status == BookStatus.Confirmed)
          .sort((b1, b2) => {
            let f = new Date(b1.Date).getTime();
            let s = new Date(b2.Date).getTime();
            return f - s;
          });
      });
    }
  
    getBookById(id: number): CustomerBook {
      return this.books.filter(b => b.Id == id)[0];
    }
  
    getStatus(id: number): string {
      let status = this.getBookById(id).Status;
      switch (status) {
        case BookStatus.Pending: return 'Pending';
        case BookStatus.Accepted: return 'Accepted';
        case BookStatus.Finished: return 'Finished';
        case BookStatus.Confirmed: return 'Rated';
        default: return 'error';
      }
    }
  
    isFinished(id: number): boolean {
      return this.getBookById(id).Status == BookStatus.Finished;
    }
  
    isRated(id: number): boolean {
      return this.getBookById(id).Status == BookStatus.Confirmed;
    }
  
    showReview(id: number) {
      let book = this.getBookById(id);
      this.modalService.open(new ReviewModal(book.Review));
    }

    openMap(id:number)
    {
     this.map = {
       center: {lat: this.books.filter(b => b.Id == id)[0].Location.Latitude, lng: this.books[0].Location.Longitude},
       zoom: 18,    
       title: this.books.find(b => b.Id === id).Work.Name,
       label: '',
       markerPos: {lat: this.books.find(b => b.Id === id).Location.Latitude, lng: this.books.find(b => b.Id === id).Location.Longitude}    
     };  
     const config = new TemplateModalConfig<IMapModal, void, void>(this.modalTemplate);
     config.isInverted = true;
     config.size = ModalSize.Tiny;
     this.currModal = this.modalService.open(config);
    }
}
