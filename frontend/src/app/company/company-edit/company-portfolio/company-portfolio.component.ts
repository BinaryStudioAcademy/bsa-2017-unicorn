import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { BookCard, BookStatus } from '../../../models/dashboard/book-card';

import { DashboardService } from '../../../services/dashboard/dashboard.service';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';
import { ReviewModal } from '../../..//review/review-modal/review-modal.component';

@Component({
  selector: 'app-company-portfolio',
  templateUrl: './company-portfolio.component.html',
  styleUrls: ['./company-portfolio.component.sass']
})
export class CompanyPortfolioComponent implements OnInit {

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
          resp.forEach(b => {
            if (b.Status == BookStatus.Confirmed) {
              this.books.push(b);
            }
          });
        }
        this.isLoaded = true;
      })
      .catch(err => this.isLoaded = true);
  }

  updateBook(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.dashboardService.update(book);
  }

  showReview(id: number) {
    let book = this.books.filter(b => b.Id == id)[0];
    this.modalService.open(new ReviewModal(book.Review));
  }

}
