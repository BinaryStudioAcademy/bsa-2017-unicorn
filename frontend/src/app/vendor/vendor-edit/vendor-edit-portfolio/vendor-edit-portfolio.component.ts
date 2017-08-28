import { Component, OnInit, Input } from '@angular/core';

import {SuiModule} from 'ng2-semantic-ui';

import { VendorService } from "../../../services/vendor.service";

import { Vendor } from '../../../models/vendor.model';
import { PortfolioItem } from '../../../models/portfolio-item.model';
import { History } from '../../../models/history';
import { VendorHistory } from "../../../models/vendor-history.model";

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

@Component({
  selector: 'app-vendor-edit-portfolio',
  templateUrl: './vendor-edit-portfolio.component.html',
  styleUrls: ['./vendor-edit-portfolio.component.sass']
})
export class VendorEditPortfolioComponent implements OnInit {
  @Input() private vendorId: number;
  
  books: BookCard[];

  constructor(
    private vendorService: VendorService,
    private dashboardService: DashboardService
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.dashboardService.getPortfolioBooks('vendor', this.vendorId).then(resp => {
      this.books = resp;
    });
  }

  updateBook(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.dashboardService.update(book);
  }
}

