import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Review } from '../../models/review.model';
import { NguiMapModule, Marker, NguiMap} from '@ngui/map';

import { SearchService } from '../../services/search.service';
import { CategoryService } from '../../services/category.service';

import { SearchWork } from '../../models/search/search-work';
import { LocationModel } from '../../models/location.model';
import { Category } from '../../models/category.model';
import { Subcategory } from '../../models/subcategory.model';


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
  /* datepicker */
  mode: string;
  firstDayOfWeek: string;
  /* advanced filters */
  companyName: string;
  reviewsChecked: boolean;
  ratingCompare: string;
  ratingFilter: number;
  categories: Category[];
  selCategories: string[];
  subcategories: Subcategory[];
  selSubcategories: string[];
  /* pagination */
  pageSize = 20;
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  works: SearchWork[] = [];
  /* map */
  positions = [];
  markers = [];
  reviewsTab = 'reviews';

  center: google.maps.LatLng;

  autocomplete: google.maps.places.Autocomplete;
  address: any = {};

  selected: string = '';

  spinner: boolean;
  loaded: boolean;
  slider: number;

  constructor(
    private searchService: SearchService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private ref: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.initDarepicker();
    this.getParameters();
    this.initAdvancedFilters();
    this.searchWorks();
  }

  searchWorks() {
    this.works = [];
    this.spinner = true;
    this.getWorksByBaseFilters(this.category, this.subcategory, this.rawDate);
  }

  getWorksByBaseFilters(category: string, subcategory: string, date: number) {
    this.searchService.getWorksByBaseFilters(category, subcategory, date)
    .then(works => {
      this.works = works;
      this.spinner = false;
      this.loaded = true;
      this.mapRedirect();
    }).catch(err => this.spinner = false);
  }

  reset() {
    this.category = undefined;
    this.subcategory = undefined;
    this.date = undefined;
    this.companyName = undefined;
    this.reviewsChecked = false;
    this.ratingCompare = 'greater';
    this.ratingFilter = 0;
    this.selCategories = [];
    this.selSubcategories = [];
    console.log(this.slider);
  }

  initAdvancedFilters() {
    this.ratingCompare = 'greater';
    this.ratingFilter = 0;
    this.categoryService.getAll()
    .then(resp => {
      this.categories = resp.body as Category[];
      console.log('categories');
      console.log(this.categories);
    });
  }

  categoriesChanged() {
    this.selSubcategories = [];
    this.subcategories = [];
    this.subcategories = getSubcategories(this.categories, this.selCategories);
    console.log(this.ratingCompare);
    console.log(this.ratingFilter);

    function getSubcategories(categories, selected) {
      let result = [];
      for (let i = 0; i < categories.length; i++) {
        for (let j = 0; j < selected.length; j++) {
          if (categories[i].Name === selected[j]) {
            result = result.concat(categories[i].Subcategories);
          }
        }
      }
      return result;
    }
  }

  subcategoriesChanged() {
    console.log('subcat: ');
    console.log(this.selSubcategories);
  }

  onMapReady(map: NguiMap) {
  }

  mapRedirect() {
    if (this.works && this.works.length > 0) {
      let loc = this.works[0].Location;
      console.log(loc.Latitude, loc.Longitude);
      this.center = new google.maps.LatLng(loc.Latitude, loc.Longitude);
      this.ref.detectChanges();
    }
  }

  selectWork(name: string, loc: LocationModel) {
    this.center = new google.maps.LatLng(loc.Latitude, loc.Longitude);
    this.selected = name;
    this.ref.detectChanges();
  }

  getWorksPage(): SearchWork[] {
    return this.works.slice((this.selectedPage - 1) * this.pageSize, this.selectedPage * this.pageSize);
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
    console.log(this.category, this.subcategory, this.date);
  }

  convertDate(date: number) {
    return new Date(1000 * date);
  }

  initDarepicker() {
    /* datepicker settings */
    this.mode = 'date';           /* select day */
    this.firstDayOfWeek = '1';    /* start calendar from first day of week */
  }
}
