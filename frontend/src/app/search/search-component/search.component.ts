import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { Validators, FormGroup, FormArray, FormBuilder } from '@angular/forms';

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
export class SearchComponent implements OnInit {
  /* query parameters */
  category: string;
  subcategory: string;
  date: Date;
  /* filter */
  filtersIsOpen: boolean;
  labelSearch: string;
  placeholderCategory: string;
  placeholderSubcategory: string;
  labelDate: string;
  mode: string;
  firstDayOfWeek: string;
  /* multi-select */
  selCat: string[];
  filterCat: string[];
  selSubcat: string[];
  filterSubcat: string[];
  /* pagination */
  pageSize = 20;
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  companies: CompanyDetails[] = [];
  /* map */
  positions = [];
  markers = [];

  autocomplete: google.maps.places.Autocomplete;
  address: any = {};

  constructor(
    private companyService: CompanyService,
    private route: ActivatedRoute,
    private ref: ChangeDetectorRef
  ) { }

  searchCompany() {
    this.companies = [];
    this.getCompanies();
  }

  resetFilters() {
    this.filtersIsOpen = false;
  }

  initialized(autocomplete: any) {
    this.autocomplete = autocomplete;
  }
  placeChanged() {
    let place = this.autocomplete.getPlace();
    console.log(place);
    for (let i = 0; i < place.address_components.length; i++) {
      let addressType = place.address_components[i].types[0];
      this.address[addressType] = place.address_components[i].long_name;
    }
    this.ref.detectChanges();
  }

  ngOnInit() {
    this.getParameters();
    this.createMockSettings();
    this.getCompanies();
    this.getCompanies();
    this.getCompanies();
    this.getCompanies();
    this.getCompanies();
    this.initContent();
  }

  getParameters() {
    this.category = this.route.snapshot.queryParams['category'];
    this.subcategory = this.route.snapshot.queryParams['subcategory'];
    if (this.route.snapshot.queryParams['date']) {
      this.date = this.convertDate(this.route.snapshot.queryParams['date']);
    }
  }

  convertDate(date: number) {
    return new Date(1000 * date);
  }

  createMockSettings() {
    this.filterCat  = ['Category 1', 'Category 2', 'Category 3', 'Category 4', 'Category 5', 'Category 6'];
    this.filterSubcat  = ['Subategory 1', 'Subategory 2', 'Subategory 3', 'Subategory 4', 'Subategory 5', 'Subategory 6'];
  }

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

  initContent() {
    this.placeholderCategory = 'SCRATCH';
    this.placeholderSubcategory = 'MY CAT';
    /* labels */
    this.labelSearch = 'What to do';
    this.labelDate = 'When to do it';
    /* datepicker settings */
    this.mode = 'date';           /* select day */
    this.firstDayOfWeek = '1';    /* start calendar from first day of week */
  }

}
