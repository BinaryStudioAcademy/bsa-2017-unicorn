import { Component, OnInit } from '@angular/core';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

@Component({
  selector: 'app-dashboard-progress',
  templateUrl: './dashboard-progress.component.html',
  styleUrls: ['./dashboard-progress.component.sass']
})
export class DashboardProgressComponent implements OnInit {

  books: BookCard[];

  constructor(private dashboardService: DashboardService) { }

  ngOnInit() {
    this.loadData();
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
    this.dashboardService.update(book).then(resp => this.loadData());
  }

}
