import { Component, OnInit, ChangeDetectorRef } from "@angular/core";

import { NguiMapModule, Marker, NguiMap} from "@ngui/map";

import { Performer } from "../../models/performer.model";
import { LocationModel } from "../../models/location.model";

import { PerformerService } from "../../services/performer.service";
import { Category } from "../../models/category.model";
import { CategoryService } from "../../services/category.service";
import { Subcategory } from "../../models/subcategory.model";
import { LocationService } from "../../services/location.service";

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.sass']
})
export class VendorsComponent implements OnInit {

  loaded: boolean;
  searchLoading: boolean

  /* pagination */
  pageSize = '20';
  totalCount: number = 0;
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  tabSuffix = '?tab=reviews';
  performers: Performer[] = [];
  reviewsTab = 'reviews';
  date: Date;
  mode = 'date';

  /* city */
  city: string;
  name: string;

  center: google.maps.LatLng;
  autocomplete: google.maps.places.Autocomplete;
  address: any = {};

  selected: string = '';

  /* advanced filters */
  role: string = "all";
  selectedCategories: number[];
  selectedSubcategories: number[];
  rating: number;
  ratingCondition: string;
  withReviews: boolean;
  slider: number;
  distance: number = 0;
  latitude: number;
  longitude: number;
  sort: string;

  filtersIsOpen: boolean;

  categories: Category[];
  subcategories: Subcategory[];

  selPerformer: Performer;
  constructor(
    private performerService: PerformerService,
    private ref: ChangeDetectorRef,
    private categoryService: CategoryService,
    private locationService: LocationService
  ) { }

  ngOnInit() {
    this.initAdvancedFilters();
    this.loadData();
  }

  loadData() {
    this.search();
  }

  onMapReady(map: NguiMap) {
  }

  mapRedirect() {
    if (this.performers && this.performers.length > 0) {
      let loc = this.performers[0].Location;
      this.center = new google.maps.LatLng(loc.Latitude, loc.Longitude);
      this.ref.detectChanges();
    }
  }

  selectPerformerLocation(name: string, loc: LocationModel) {
    this.center = new google.maps.LatLng(loc.Latitude, loc.Longitude);
    this.selected = name;
    this.ref.detectChanges();
  }

  getPerformersPage(): Performer[] {
    const pageSize = Number(this.pageSize);
    return this.performers.slice((this.selectedPage - 1) * pageSize, this.selectedPage * pageSize);
  }

  getPageSize() {
    return Number(this.pageSize);
  }

  pageSizeChanged() {
    this.selectedPage = 1;
    this.search();
  }

  search() {
    this.searchLoading = true;
    this.filtersIsOpen = false;
    let filteredCategories = this.selectedCategories || [];

    if (this.selectedSubcategories) {
      this.selectedSubcategories
        .forEach(sctgId => 
          filteredCategories = filteredCategories.filter(ctgId => 
            this.categories.find(c => c.Id === ctgId).Subcategories
              .find(s => s.Id === sctgId) === undefined));
        }

    let date; 
    let timeZone;   
    if(this.date){      
      date = new Date(this.date).toJSON();
      timeZone = this.date.getTimezoneOffset();
    }
    else{ 
      date = null;
      timeZone = 0;
    }   
     
    return this.performerService
      .getPerformersByFilters(
        this.city, this.name, this.role, this.rating, this.ratingCondition, this.withReviews, filteredCategories, this.selectedSubcategories, 
        this.selectedPage, Number(this.pageSize), this.latitude, this.longitude, this.distance, this.sort, date, timeZone
      )
      .then(resp => {
        this.performers = resp.Items;
        // this.performers = this.performers.concat(resp.Items); // for debugging should be deleted
        this.selectedPage = resp.CurrentPage;
        this.pageSize = resp.PageSize.toString();
        this.totalCount = resp.TotalCount;


        this.searchLoading = false;
        this.mapRedirect();
        this.ref.detectChanges();
      })
      .catch(err => this.searchLoading = false);
  }

  reset() {
    this.city = '';
    this.name = '';
    this.selectedCategories = [];
    this.selectedSubcategories = [];
    this.ratingCondition = 'greater';
    this.rating = undefined;
    this.latitude = undefined;
    this.longitude = undefined;
    this.slider = 0;
    this.distance = 0;
    this.withReviews = false;
    this.search();
  }

  initAdvancedFilters() {
    this.ratingCondition = 'greater';
    this.rating = 0;
    this.slider = 0;
    this.categoryService.getAll()
    .then(resp => {
      this.categories = resp.body as Category[];
    });
    this.sort = 'rating';
  }

  initialized(autocomplete: any) {
    this.autocomplete = autocomplete;
  }

  categoriesChanged() {
    this.selectedSubcategories = [];
    this.subcategories = [];
    this.selectedCategories
      .forEach(id => this.subcategories = this.subcategories.concat(this.categories.find(c => c.Id === id).Subcategories));
  }

  placeChanged(event) {
    this.locationService.getLocDetails(event.geometry.location.lat(), event.geometry.location.lng())
      .subscribe(result => {
        this.city = result
          .address_components[result.address_components.findIndex(x => x.types.length === 2 && x.types.includes("locality") && x.types.includes("political"))]
          .short_name;
        this.longitude = event.geometry.location.lng();
        this.latitude = event.geometry.location.lat();

        this.search();
        this.ref.detectChanges();
      });
  }

  scrollToElement(id) {
    const element = document.querySelector('#' + id);
    element.scrollIntoView(false);
  }

  highlight(performer) {
    if (this.selPerformer && this.selPerformer.PerformerType === performer.PerformerType && this.selPerformer.Id === performer.Id) {
      return true;
    } else {
      return false;
    }
  }

  markerHandle(per) {
    this.selPerformer = per;
    this.selected = per.Name;
    this.scrollToElement(per.PerformerType + per.Id);
  }
}
