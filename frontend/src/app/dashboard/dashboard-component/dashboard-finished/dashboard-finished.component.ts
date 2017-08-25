import { Component, OnInit } from '@angular/core';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

@Component({
  selector: 'app-dashboard-finished',
  templateUrl: './dashboard-finished.component.html',
  styleUrls: ['./dashboard-finished.component.sass']
})
export class DashboardFinishedComponent implements OnInit {
 
  books: BookCard[];
  
  constructor(private dashboardService: DashboardService) { }
  
  ngOnInit() {
    this.dashboardService.getFinishedBooks().then(resp => {
      this.books = resp;
      console.log(this.books);
    });
  }

}
