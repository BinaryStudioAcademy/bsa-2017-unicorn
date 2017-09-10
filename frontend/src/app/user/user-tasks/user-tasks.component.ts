import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { NgModel } from '@angular/forms';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../review/review-modal/review-modal.component';

import { CustomerbookService } from '../../services/customerbook.service';
import { ReviewService } from '../../services/review.service';
import { TaskMessagingService } from '../../services/task-messaging.service';

import { CustomerBook, BookStatus } from '../../models/book/book.model';
import { ShortReview } from '../../models/short-review';
import { NotificationService } from "../../services/notifications/notification.service";

export interface IReviewContext {
  id: number;
}

export interface IDeclinedReasonContext {
  reason: string;
}

@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrls: ['./user-tasks.component.sass']
})
export class UserTasksComponent implements OnInit {
  
  @ViewChild('modalTemplate')
  public modalTemplate:ModalTemplate<IReviewContext, void, void>
  @ViewChild('reasonModal')
  public reasonModal:ModalTemplate<IDeclinedReasonContext, void, void>

  @Input() user:User;

  currModal: SuiActiveModal<IReviewContext, {}, void>;
  
  review: ShortReview = {
    BookId: 0,
    Grade: 0,
    PerformerId: 0,
    PerformerType: '',
    Text: ''
  };

  loader: boolean;
  error: boolean;
  deleteLoader: boolean;
  deleteLoads: {[id: number]: boolean} = {};

  books: CustomerBook[];

  isLoaded: boolean;

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
  }

  loadData() {
    this.isLoaded = false;
    this.bookService.getCustomerBooks(this.user.Id)
    .then(resp => {
      this.books = resp.filter(b => b.Status != BookStatus.Confirmed)
        .sort((b1, b2) => b1.Status - b2.Status)
        .sort((b1, b2) => {
          if (b1.Status !== b2.Status) return 0;
          let f = new Date(b1.Date).getTime();
          let s = new Date(b2.Date).getTime();
          return s - f;
        });
      this.isLoaded = true;
      console.log(resp);
    })
    .catch(err => {
      this.isLoaded = true;
    });
  }

  getBookById(id: number): CustomerBook {
    return this.books.filter(b => b.Id == id)[0];
  }

  getStatus(id: number): string {
    let status = this.getBookById(id).Status;
    switch (status) {
      case BookStatus.Pending: return 'Pending';
      case BookStatus.Declined: return 'Declined';
      case BookStatus.Accepted: return 'Accepted';
      case BookStatus.Finished: return 'Finished';
      case BookStatus.Confirmed: return 'Rated';
      default: return 'error';
    }
  }

  isFinished(id: number): boolean {
    return this.getBookById(id).Status == BookStatus.Finished;
  }

  isReason(book: CustomerBook): boolean {
    return book.Status == BookStatus.Declined && book.DeclinedReason !== undefined && book.DeclinedReason !== null;
  }

  isRated(id: number): boolean {
    return this.getBookById(id).Status == BookStatus.Confirmed;
  }

  openModal(bookId: number) {
    const config = new TemplateModalConfig<IReviewContext, void, void>(this.modalTemplate);
    config.context = {id: bookId};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }

  showReview(id: number) {
    let book = this.getBookById(id);
    this.modalService.open(new ReviewModal(book.Review))
      .onDeny(this.clearData);
  }

  showReason(reason: string) {
    const config = new TemplateModalConfig<IDeclinedReasonContext, void, void>(this.reasonModal);
    config.context = {reason: reason};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }

  clearData() {
    this.error = false;
    this.review.Grade = 0;
    this.review.Text = '';
  }

  saveReview(id: number) {
    if (this.review.Grade == 0) {
      this.error = true;
      return;
    }
    this.loader = true;
    let book = this.getBookById(id);
    this.review.BookId = id;
    this.review.PerformerId = book.PerformerId;
    this.review.PerformerType = book.PerformerType;
    this.reviewService.saveReview(this.review).then(resp => {
      this.books.splice(this.books.findIndex(b => b.Id === id), 1);
      this.taskMessaging.finishTask();
      this.loader = false;
      this.currModal.deny(undefined);
      this.clearData();
    }).catch(err => {
      this.loader = false;
      this.currModal.deny(undefined);
      this.clearData();
    });
  }

  deleteDeclinedBook(book: CustomerBook) {
    this.deleteLoads[book.Id] = true;
    this.bookService.deleteBook(book).then(res => {
      this.books.splice(this.books.findIndex(b => b.Id === book.Id), 1);
      this.deleteLoader;
      this.deleteLoads[book.Id] = false;
    }).catch(err => {
      this.deleteLoads[book.Id] = false;
    });
  }

  isDeleting(book: CustomerBook): boolean {
    return this.deleteLoads[book.Id];
  }

}
