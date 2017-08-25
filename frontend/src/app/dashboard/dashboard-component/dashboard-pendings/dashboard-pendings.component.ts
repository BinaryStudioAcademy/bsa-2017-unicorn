import { Component, OnInit } from '@angular/core';

import { BookCard } from '../../../models/dashboard/book-card';

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
    this.dashboardService.getVendorBooks(1, 'vendor').then(resp => {
      this.books = resp;
      console.log(this.books);
    });
  }

}
