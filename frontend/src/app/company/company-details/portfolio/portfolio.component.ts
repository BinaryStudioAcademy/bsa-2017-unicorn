import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../..//review/review-modal/review-modal.component';

@Component({
  selector: 'company-portfolio',
  templateUrl: './portfolio.component.html',
  styleUrls: ['./portfolio.component.sass']
})
export class PortfolioComponent implements OnInit {

  books: BookCard[] = [];
  companyId: number;

  isLoaded: boolean;

  constructor(
    private dashboardService: DashboardService,
    private route: ActivatedRoute,
    private modalService: SuiModalService
  ) { }

  ngOnInit() {
    this.companyId = this.route.snapshot.params['id'];
    this.loadData();
  }

  loadData() {
    this.isLoaded = false;
    this.dashboardService.getPortfolioBooks('company', this.companyId)
      .then(resp => {
        if(resp !== null){
          this.books = resp.filter(b => b.IsHidden == false && b.Status == BookStatus.Confirmed);
        }
        this.isLoaded = true
      })
      .catch(err => this.isLoaded = true);
  }

  showReview(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.modalService.open(new ReviewModal(book.Review));
  }

}
