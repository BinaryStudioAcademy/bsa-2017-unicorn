import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Review } from '../../models/review.model';
import { NguiMapModule, Marker } from '@ngui/map';
// import { MapModel } from '../../models/map.model';

import { ISubscription } from 'rxjs/Subscription';
import { CompanyService } from '../../services/company-services/company.service';
import { CompanyShort } from '../../models/company-page/company-short.model';
import { CompanyDetails } from '../../models/company-page/company-details.model';

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
  /* filter */
  labelSearch: string;
  placeholderCategory: string;
  placeholderSubcategory: string;
  searchCategory: string;
  searchSubcategory: string;
  labelDate: string;
  searchDate: Date;
  mode: string;
  firstDayOfWeek: string;
  /* multi-select */
  selCat: string[];
  filterCat: string[];
  selSubcat: string[];
  filterSubcat: string[];
  /* pagination */
  pageSize = 12;
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  // companies: CompanyShort[] = [];
  companies: CompanyDetails[] = [];
  /* map */
  positions = [];
  markers = [];

  private subscription: ISubscription;

  constructor(
    private companyService: CompanyService,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.getParameters();
    this.createMockSettings();
    // this.createMockCompanies(20);
    this.getCompanies();
    this.initContent();
  }

  getParameters() {
    this.category = this.route.snapshot.queryParams['category'];
    this.subcategory = this.route.snapshot.queryParams['subcategory'];
    this.date = this.convertDate(this.route.snapshot.queryParams['date']);
  }

  convertDate(date: number) {
    return new Date(1000 * date);
  }

  createMockSettings() {
    this.filterCat  = ['Category 1', 'Category 2', 'Category 3', 'Category 4', 'Category 5', 'Category 6'];
    this.filterSubcat  = ['Subategory 1', 'Subategory 2', 'Subategory 3', 'Subategory 4', 'Subategory 5', 'Subategory 6'];
  }

  // createMockCompanies(num: number) {
  //   let i: number;
  //   for (i = num; i >= 1; i--) {
  //     const company = this.companyService.getMockCompany();
  //     this.companies.push(company);

  //     const marker = {
  //       pos: [company.Location.Latitude, company.Location.Longitude],
  //       title: company.Name
  //     };
  //     this.markers.push(marker);
  //     // this.positions.push([company.Location.Latitude, company.Location.Longitude]);
  //   }
  // }

  getCompanies() {
    this.companyService.getCompanies()
    .then(
      companies => {
        companies.forEach(e => {
          this.companyService.getCompanyDetails(e.Id)
          .then(
            detail => {
              this.companies.push(detail);
            }
          );
        });
        console.log(this.companies);
      }
    );
  }

  // getCompaniesOld() {
  //   this.companyService.getCompanies()
  //   .then(
  //     companies => {
  //       this.companies = companies;
  //       console.log(this.companies);
  //     }
  //   );
  // }

  initContent() {
    this.placeholderCategory = 'SCRATCH';
    this.placeholderSubcategory = 'MY CAT';
    this.searchCategory = '';
    this.searchSubcategory = '';
    /* labels */
    this.labelSearch = 'What to do';
    this.labelDate = 'When to do it';
    /* datepicker settings */
    this.mode = 'date';           /* select day */
    this.firstDayOfWeek = '1';    /* start calendar from first day of week */
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
