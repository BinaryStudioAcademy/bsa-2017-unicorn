import { Component, OnInit } from '@angular/core';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
@Component({
  selector: 'app-dashboard-pendings',
  templateUrl: './dashboard-pendings.component.html',
  styleUrls: ['./dashboard-pendings.component.sass']
})
export class DashboardPendingsComponent implements OnInit {

  books: BookCard[];

  constructor(private dashboardService: DashboardService) { }

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
    this.dashboardService.update(book).then(resp => this.loadData());
  }

  decline(id: number) {
    let book: BookCard = this.books.filter(b => b.Id == id)[0];
    book.Status = BookStatus.Declined;
    this.dashboardService.update(book).then(resp => this.loadData());
  }

}
