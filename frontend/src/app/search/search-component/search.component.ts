import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Review } from '../../models/review.model';
import { NguiMapModule, Marker } from '@ngui/map';

import { SearchService } from '../../services/search.service';
import { SearchPerformer } from '../../models/search/search-performer';


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
  rawDate: number;
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
  performers: SearchPerformer[] = [];
  tabSuffix = '?tab=reviews';
  /* map */
  positions = [];
  markers = [];

  autocomplete: google.maps.places.Autocomplete;
  address: any = {};

  constructor(
    private searchService: SearchService,
    private route: ActivatedRoute,
    private ref: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.getParameters();
    this.createMockSettings();
    this.searchPerformers();
  }

  searchPerformers() {
    this.searchService.getSearchPerformers()
    .then(performers => {
      this.performers = performers;
    });
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


  getParameters() {
    this.category = this.route.snapshot.queryParams['category'];
    this.subcategory = this.route.snapshot.queryParams['subcategory'];
    if (this.route.snapshot.queryParams['date']) {
      this.rawDate = this.route.snapshot.queryParams['date'];
      this.date = this.convertDate(this.rawDate);
    }
  }

  convertDate(date: number) {
    return new Date(1000 * date);
  }

  createMockSettings() {
    this.filterCat  = ['Category 1', 'Category 2', 'Category 3', 'Category 4', 'Category 5', 'Category 6'];
    this.filterSubcat  = ['Subategory 1', 'Subategory 2', 'Subategory 3', 'Subategory 4', 'Subategory 5', 'Subategory 6'];
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
