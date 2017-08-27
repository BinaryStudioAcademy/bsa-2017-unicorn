import { Component, OnInit, Input, AfterContentInit } from '@angular/core';

import {SuiModule} from 'ng2-semantic-ui';

import { VendorService } from "../../../services/vendor.service";

import { Vendor } from '../../../models/vendor.model';
import { PortfolioItem } from '../../../models/portfolio-item.model';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

@Component({
  selector: 'app-vendor-profile-portfolio',
  templateUrl: './vendor-profile-portfolio.component.html',
  styleUrls: ['./vendor-profile-portfolio.component.sass']
})
export class VendorProfilePortfolioComponent implements OnInit, AfterContentInit {
  @Input() private vendorId: number;
  
  portfolio: PortfolioItem[];
  books: BookCard[];

  constructor(
    private vendorService: VendorService,
    private dashboardService: DashboardService
  ) { }

  ngAfterContentInit(): void {
    // this.vendorService.getVendorPorfolio(this.vendorId)
    //   .then(resp => this.portfolio = resp.body as PortfolioItem[]);
    this.loadData();
  }

  loadData() {
    this.dashboardService.getPortfolioBooks('vendor', this.vendorId).then(resp => {
      this.books = resp;
      console.log(this.books);
    });
  }

  ngOnInit() {
  }
}
