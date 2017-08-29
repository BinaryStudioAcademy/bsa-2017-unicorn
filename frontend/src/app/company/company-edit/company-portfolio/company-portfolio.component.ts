import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

@Component({
  selector: 'app-company-portfolio',
  templateUrl: './company-portfolio.component.html',
  styleUrls: ['./company-portfolio.component.sass']
})
export class CompanyPortfolioComponent implements OnInit {

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
      this.books = resp;
    });
  }

  updateBook(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.dashboardService.update(book);
  }

}
