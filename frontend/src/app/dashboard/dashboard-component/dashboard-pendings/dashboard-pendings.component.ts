import { Component, OnInit } from '@angular/core';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';
import { DashboardService } from '../../../services/dashboard/dashboard.service';

import {ToastsManager, Toast} from 'ng2-toastr';
import {ToastOptions} from 'ng2-toastr';

@Component({
  selector: 'app-dashboard-pendings',
  templateUrl: './dashboard-pendings.component.html',
  styleUrls: ['./dashboard-pendings.component.sass']
})
export class DashboardPendingsComponent implements OnInit {

  books: BookCard[];

  aloads: {[bookId: number]: boolean} = {};
  dloads: {[bookId: number]: boolean} = {};

  constructor(
    private dashboardService: DashboardService,
    private dashMessaging: DashMessagingService,
    private toastr: ToastsManager
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.dashboardService.getPendingBooks().then(resp => {
      this.books = resp;
      console.log(this.books);
    });
  }

  accept(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Accepted;
    this.aloads[book.Id] = true;
    this.dashboardService.update(book).then(resp => {
      this.loadData();
      this.aloads[book.Id] = false;
      this.dashMessaging.changePending();
      this.toastr.success('Accepted task');
    }).catch(err => this.toastr.error('Ops. Cannot accept task'));
  }

  decline(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Declined;
    this.dloads[book.Id] = true;
    this.dashboardService.update(book).then(resp => {
      this.loadData();
      this.dloads[book.Id] = false;
      this.toastr.success('Declined task');
    }).catch(err => this.toastr.error('Ops. Cannot decline task'));
  }

}
