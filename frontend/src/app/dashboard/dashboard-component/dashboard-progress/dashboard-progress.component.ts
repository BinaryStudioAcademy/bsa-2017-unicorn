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
    this.dashboardService.getAcceptedBooks().then(resp => {
      this.books = resp;
      console.log(this.books);
    });
  }

}
