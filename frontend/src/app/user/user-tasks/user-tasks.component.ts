import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { NgModel } from '@angular/forms';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../review/review-modal/review-modal.component';

import { CustomerbookService } from '../../services/customerbook.service';
import { ReviewService } from '../../services/review.service';

import { CustomerBook, BookStatus } from '../../models/book/book.model';
import { ShortReview } from '../../models/short-review';

export interface IContext {
  id: number;
}

@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrls: ['./user-tasks.component.sass']
})
export class UserTasksComponent implements OnInit {
  
  @ViewChild('modalTemplate')
  public modalTemplate:ModalTemplate<IContext, void, void>

  @Input() user:User;

  currModal: SuiActiveModal<IContext, {}, void>;
  
  review: ShortReview = {
    BookId: 0,
    Grade: 0,
    PerformerId: 0,
    PerformerType: '',
    Text: ''
  };

  loader: boolean;
  error: boolean;

  books: CustomerBook[];

  constructor(
    private bookService: CustomerbookService,
    private modalService: SuiModalService,
    private reviewService: ReviewService
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.bookService.getCustomerBooks(this.user.Id)
    .then(resp => {
      this.books = resp.filter(b => b.Status != BookStatus.Confirmed)
        .sort((b1, b2) => b1.Status - b2.Status)
        .sort((b1, b2) => {
          if (b1.Status !== b2.Status) return 0;
          let f = new Date(b1.Date).getTime();
          let s = new Date(b2.Date).getTime();
          return f - s;
        });
      console.log(resp);
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

  openModal(bookId: number) {
    const config = new TemplateModalConfig<IContext, void, void>(this.modalTemplate);
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
      this.loadData();
      this.loader = false;
      this.currModal.deny(undefined);
      this.clearData();
    }).catch(err => {
      this.loader = false;
      this.currModal.deny(undefined);
      this.clearData();
    });
  }

}
