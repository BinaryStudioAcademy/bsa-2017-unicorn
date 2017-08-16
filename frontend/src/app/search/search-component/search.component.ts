import { Component, OnInit } from '@angular/core';

import { Company } from '../../models/company.model';
import { Review } from '../../models/review.model';
import { CompanyService } from '../../services/company.service';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {
  companies: Company[] = [];
  /* pagination */
  pageSize = 12;
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;

  constructor(
    private companyService: CompanyService
  ) { }

  ngOnInit() {
    this.createMockCompanies(20);
  }

  createMockCompanies(num: number) {
    let i: number;
    for (i = num; i >= 1; i--) {
      this.companies.push(this.companyService.getCompany());
    }
  }

}
