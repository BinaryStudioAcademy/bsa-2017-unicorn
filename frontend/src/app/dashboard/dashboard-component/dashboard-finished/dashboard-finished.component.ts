import { Component, OnInit, OnDestroy, ViewChild } from '@angular/core';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize} from 'ng2-semantic-ui';
import { ReviewModal } from '../../../review/review-modal/review-modal.component';

import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-dashboard-finished',
  templateUrl: './dashboard-finished.component.html',
  styleUrls: ['./dashboard-finished.component.sass']
})
export class DashboardFinishedComponent implements OnInit, OnDestroy {
 
  books: BookCard[];
  sub: Subscription;
  
  constructor(
    private dashboardService: DashboardService,
    private dashMessaging: DashMessagingService,
    public modalService: SuiModalService
  ) { }
  
  ngOnInit() {
    this.loadData();
    this.sub = this.dashMessaging.progressEvent$.subscribe(() => {
      this.loadData();
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  loadData() {
    this.dashboardService.getFinishedBooks().then(resp => {
      this.books = resp;
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

}
