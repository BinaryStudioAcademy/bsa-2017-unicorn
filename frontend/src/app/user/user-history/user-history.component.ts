import { Component, OnInit, Input } from '@angular/core';
import { NgModel } from '@angular/forms';
import { User } from '../../models/user';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../review/review-modal/review-modal.component';

import { CustomerbookService } from '../../services/customerbook.service';
import { ReviewService } from '../../services/review.service';
import { NotificationService } from "../../services/notifications/notification.service";

import { CustomerBook, BookStatus } from '../../models/book/book.model';
import { ShortReview } from '../../models/short-review';

@Component({
  selector: 'app-user-history',
  templateUrl: './user-history.component.html',
  styleUrls: ['./user-history.component.sass']
})
export class UserHistoryComponent implements OnInit {

  @Input() user: User;
  
  books: CustomerBook[];
  
    constructor(
      private bookService: CustomerbookService,
      private modalService: SuiModalService,
      private reviewService: ReviewService,
      private notificationService: NotificationService
    ) { }
  
    ngOnInit() {
      this.loadData();
      this.notificationService.listen<any>("RefreshOrders", () => this.loadData());
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

}
