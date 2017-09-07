import { Component, OnInit, Input, AfterContentInit } from '@angular/core';

import {SuiModule} from 'ng2-semantic-ui';

import { Vendor } from '../../../models/vendor.model';
import { PortfolioItem } from '../../../models/portfolio-item.model';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

import { VendorService } from "../../../services/vendor.service";

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../..//review/review-modal/review-modal.component';

@Component({
  selector: 'app-vendor-profile-portfolio',
  templateUrl: './vendor-profile-portfolio.component.html',
  styleUrls: ['./vendor-profile-portfolio.component.sass']
})
export class VendorProfilePortfolioComponent implements OnInit, AfterContentInit {
  @Input() private vendorId: number;

  books: BookCard[] = [];

  isLoaded: boolean;

  constructor(
    private vendorService: VendorService,
    private dashboardService: DashboardService,
    private modalService: SuiModalService
  ) { }

  ngAfterContentInit(): void {
    this.loadData();
  }

  loadData() {
    this.isLoaded = false
    this.dashboardService.getPortfolioBooks('vendor', this.vendorId).then(resp => {
      this.books = resp.filter(b => b.IsHidden == false && b.Status == BookStatus.Confirmed);
      this.isLoaded = true;
    })
    .catch(err => this.isLoaded = true)
  }

  showReview(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.modalService.open(new ReviewModal(book.Review));
  }

  ngOnInit() {
  }
}
