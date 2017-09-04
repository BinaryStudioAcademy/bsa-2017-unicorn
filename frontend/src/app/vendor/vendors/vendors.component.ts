import { Component, OnInit, ChangeDetectorRef } from "@angular/core";

import { NguiMapModule, Marker, NguiMap} from "@ngui/map";

import { Performer } from "../../models/performer.model";
import { LocationModel } from "../../models/location.model";

import { PerformerService } from "../../services/performer.service";
import { Category } from "../../models/category.model";
import { CategoryService } from "../../services/category.service";
import { Subcategory } from "../../models/subcategory.model";

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
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  tabSuffix = '?tab=reviews';
  performers: Performer[] = [];
  reviewsTab = 'reviews';

  /* city */
  city: string;
  name: string;

  center: google.maps.LatLng;

  autocomplete: google.maps.places.Autocomplete;
  address: any = {};

  selected: string = '';

  /* advanced filters */
  role: string;
  selectedCategories: number[];
  selectedSubcategories: number[];
  rating: number;
  ratingCondition: string;
  withReviews: boolean;
  slider: number;
  distance: number;
  latitude: number;
  longitude: number;
  sort: string;

  categories: Category[];
  subcategories: Subcategory[];

  constructor(
    private performerService: PerformerService,
    private ref: ChangeDetectorRef,
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.initAdvancedFilters();
    this.loadData();
  }

  loadData() {
    this.performerService.getAllPerformers()
    .then(resp => {
      console.log(resp);
      this.performers = resp;
      this.loaded = true;
      this.mapRedirect();
    });
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

  select(name: string, loc: LocationModel) {
    this.center = new google.maps.LatLng(loc.Latitude, loc.Longitude);
    this.selected = name;
    this.ref.detectChanges();
  }

  getPerformersPage(): Performer[] {
    const pageSize = Number(this.pageSize);
    return this.performers.slice((this.selectedPage - 1) * pageSize, this.selectedPage * pageSize);
  }

  search() {
    this.searchLoading = true;
    return this.performerService.getPerformersByFilters(this.city, this.name, this.role, this.rating, this.withReviews, this.selectedCategories)
      .then(resp => {
        this.performers = resp;
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
    this.distance = undefined;
    this.withReviews = false;
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

  convertRatingType(rating: string) {
    switch (rating) {
      case 'greater':
        return 'ge';
      case 'lower':
        return 'le';
      default:
        return 'ge';
    }
  }

  categoriesChanged() {
    this.selectedSubcategories = [];
    this.subcategories = [];
    this.selectedCategories
      .forEach(id => this.subcategories = this.subcategories.concat(this.categories.find(c => c.Id === id).Subcategories));
  }

  placeChanged(event) {
    this.search();
    this.ref.detectChanges();
  }

}
