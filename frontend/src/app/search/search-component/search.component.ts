import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Company } from '../../models/company.model';
import { Review } from '../../models/review.model';
import { CompanyService } from '../../services/company.service';
import { NguiMapModule, Marker } from '@ngui/map';
// import { MapModel } from '../../models/map.model';

import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.sass']
})
export class SearchComponent implements OnInit, OnDestroy {
  /* query parameters */
  category: string;
  subcategory: string;
  date: Date;
  /* pagination */
  pageSize = 12;
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  companies: Company[] = [];
  /* map */

  private subscription: ISubscription;

  constructor(
    private companyService: CompanyService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.getParameters();
    this.createMockCompanies(100);
  }

  getParameters() {
    this.category = this.route.snapshot.queryParams['category'];
    this.subcategory = this.route.snapshot.queryParams['subcategory'];
    this.date = this.convertDate(this.route.snapshot.queryParams['date']);
  }

  convertDate(date: number) {
    return new Date(1000 * date);
  }

  createMockCompanies(num: number) {
    let i: number;
    for (i = num; i >= 1; i--) {
      this.companies.push(this.companyService.getMockCompany());
    }
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
