import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

@Component({
  selector: 'company-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.sass']
})
export class PortfolioComponent implements OnInit {

  books: BookCard[] = [];
  companyId: number;

  constructor(
    private dashboardService: DashboardService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.companyId = this.route.snapshot.params['id'];
    this.loadData();
  }

  loadData() {
    this.dashboardService.getPortfolioBooks('company', this.companyId).then(resp => {
      this.books = resp.filter(b => b.IsHidden == false);
      console.log(this.books);
    });
  }

}
