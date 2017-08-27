import { Component, OnInit, OnDestroy } from '@angular/core';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';
import { DashMessagingService } from '../../../services/dashboard/dash-messaging.service';

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
    private dashMessaging: DashMessagingService
  ) { }
  
  ngOnInit() {
    this.loadData();
    this.sub = this.dashMessaging.progressEvent$.subscribe(() => {
      this.loadData();
    });
  }

  loadData() {
    this.dashboardService.getFinishedBooks().then(resp => {
      this.books = resp;
      console.log(this.books);
    });
  }

  ngOnDestroy() {
    this.sub.unsubscribe();
  }

}
