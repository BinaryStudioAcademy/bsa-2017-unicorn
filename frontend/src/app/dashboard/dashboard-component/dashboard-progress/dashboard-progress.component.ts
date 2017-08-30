import { Component, OnInit, OnDestroy } from '@angular/core';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';
import { NotificationService } from "../../../services/notifications/notification.service";

import {ToastsManager, Toast} from 'ng2-toastr';
import {ToastOptions} from 'ng2-toastr';

import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-dashboard-progress',
  templateUrl: './dashboard-progress.component.html',
  styleUrls: ['./dashboard-progress.component.sass']
})
export class DashboardProgressComponent implements OnInit, OnDestroy {

  books: BookCard[];
  loads: {[bookId: number]: boolean} = {};
  sub: Subscription;

  constructor(
    private dashboardService: DashboardService,
    private dashMessaging: DashMessagingService,
    private notificationService: NotificationService,
    private toastr: ToastsManager) { }

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
      this.books = resp;
      console.log(this.books);
    });
  }

  finish(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Finished;
    this.loads[book.Id] = true;
    this.dashboardService.update(book).then(resp => {
      this.loadData();
      this.loads[book.Id] = false;
      this.dashMessaging.changeProgress();
      this.toastr.success('Finished task');
    }).catch(err => this.toastr.error('Cannot finish task'));
  }

}
