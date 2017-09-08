import { Component, OnInit, Input } from '@angular/core';

import {SuiModule} from 'ng2-semantic-ui';

import { VendorService } from "../../../services/vendor.service";
import { DashboardService } from '../../../services/dashboard/dashboard.service';

import { Vendor } from '../../../models/vendor.model';
import { PortfolioItem } from '../../../models/portfolio-item.model';
import { History } from '../../../models/history';
import { VendorHistory } from "../../../models/vendor-history.model";
import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../..//review/review-modal/review-modal.component';


@Component({
  selector: 'app-vendor-edit-portfolio',
  templateUrl: './vendor-edit-portfolio.component.html',
  styleUrls: ['./vendor-edit-portfolio.component.sass']
})
export class VendorEditPortfolioComponent implements OnInit {
  @Input() private vendorId: number;
  
  books: BookCard[] = [];

  isLoaded: boolean;

  constructor(
    private vendorService: VendorService,
    private dashboardService: DashboardService,
    private modalService: SuiModalService
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.isLoaded = false;
    this.dashboardService.getPortfolioBooks('vendor', this.vendorId)
      .then(resp => {
        this.books = resp.filter(b => b.Status == BookStatus.Confirmed);
        this.isLoaded = true;
      })
      .catch(err => this.isLoaded = true);;
  }

  showReview(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.modalService.open(new ReviewModal(book.Review));
  }

  updateBook(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.dashboardService.update(book);
  }
}

