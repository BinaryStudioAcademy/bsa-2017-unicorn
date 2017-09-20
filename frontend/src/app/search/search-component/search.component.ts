import { Component, ChangeDetectorRef, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Review } from '../../models/review.model';
import { NguiMapModule, Marker, NguiMap} from '@ngui/map';

import { SearchService } from '../../services/search.service';
import { CategoryService } from '../../services/category.service';
import { LocationService } from "../../services/location.service";

import { SearchWork } from '../../models/search/search-work';
import { SearchTag } from '../../models/search/search-tag';
import { SearchMarker } from '../../models/search/search-marker';
import { LocationModel } from '../../models/location.model';
import { Category } from '../../models/category.model';
import { Subcategory } from '../../models/subcategory.model';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.sass']
})
export class SearchComponent implements OnInit {
  spinner: boolean;
  loaded: boolean;
  /* search autocomplete */
  filterCtgs: SearchTag[] = [];
  filterSubctgs: SearchTag[] = [];
  /* query parameters */
  category: string;
  subcategory: string;
  date: Date;
  rawDate: number;
  /* datepicker */
  mode: string;
  firstDayOfWeek: string;
  /* advanced filters */
  togglePanel: boolean;
  vendorName: string;
  ratingCompare: string;
  ratingCmp: string;
  rating: number;
  reviewsChecked: boolean;
  latitude: number;
  longitude: number;
  slider: number;
  distance: number;
  categories: Category[];
  selCategories: string[];
  subcategories: Subcategory[];
  selSubcategories: string[];
  sort: string;
  selSort: number;
  city: string;
  /* pagination */
  pageSize = '20';
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  works: SearchWork[] = [];
  pagedWorks: SearchWork[] = [];
  /* map */
  positions = [];
  markers = [];
  selMarker: SearchMarker;
  searchMarkers: SearchMarker[] = [];
  reviewsTab = 'reviews';
  center: google.maps.LatLng;
  autocomplete: google.maps.places.Autocomplete;
  address: any = {};
  place: any;
  selected: string = '';
  selWork: string;

  constructor(
    private searchService: SearchService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private ref: ChangeDetectorRef,
    private locationService: LocationService
  ) { }

  ngOnInit() {
    this.initDarepicker();
    this.getParameters();
    this.initAdvancedFilters();
    this.loadWorks();
  }

  loadWorks() {
    this.works = [];
    this.spinner = true;
    let date = null;
    let timeZone = 0;
    this.getWorksByBaseFilters(this.category, this.subcategory, date, timeZone);    
    this.pagedWorks = this.getWorksPage();
    this.searchMarkers = this.getMarkers();
  }

  getWorksByBaseFilters(category: string, subcategory: string, date: string, timeZone: number) {
    this.works = [];
    this.searchService.getWorksByBaseFilters(category, subcategory, date, timeZone)
    .then(works => {      
      this.works = works;
      this.pagedWorks = this.getWorksPage();
      this.searchMarkers = this.getMarkers();
      this.spinner = false;
      this.loaded = true;
      this.mapRedirect();
      this.ref.detectChanges();
    }).catch(err => this.spinner = false);
  }

  filter(arr, search = '') {
    const result = [];
    if (arr) {
      if (search !== '') {
        for (let i = 0; i < arr.length; i++) {
          const tags = arr[i].Tags.split(',');
          for (let j = 0; j < tags.length; j++) {
            const tag = tags[j].toLowerCase();
            let input = search.toLowerCase();
            if (tag.indexOf(input) > -1) {
              let start = tag.substring(0, tag.indexOf(input));
              const end = tag.substring(tag.indexOf(input) + input.length);
              if (start.length > 0) {
                start = this.capitalizeFirstLetter(start);
              } else {
                input = this.capitalizeFirstLetter(input);
              }
              const html = start + '<b>' + input + '</b>' + end;
              const tagObj = {
                Name: tags[j],
                Html: html,
                Value: arr[i].Name,
                Group: arr[i].Name,
                Icon: arr[i].Icon
              };
              result.push(tagObj);
              if (result.length > 30) {
                return result;
              }
            }
          }
        }
      } else {
        for (let i = 0; i < arr.length; i++) {
          const tagObj = {
            Name: arr[i].Name,
            Html: arr[i].Name,
            Value: arr[i].Name,
            Group: '',
            Icon: arr[i].Icon
          };
          result.push(tagObj);
          if (result.length > 30) {
            return result;
          }
        }
      }
    }
    return result;
  }

  filterCategory() {
    this.filterCtgs = this.filter(this.categories, this.category);
  }

  filterSubcategory() {
    let subcategories = [];
    if (this.category) {
      const category = this.categories.find(c => c.Name === this.category);
      if (category) {
        subcategories = category.Subcategories;
      } else {
        return;
      }
    } else {
      subcategories = getAllSubcategories(this.categories);
    }
    if (subcategories) {
      this.filterSubctgs = this.filter(subcategories, this.subcategory);
    }

    function getAllSubcategories(categories) {
      let result = [];
      if (categories) {
        for (let i = 0; i < categories.length; i++) {
          result = result.concat(categories[i].Subcategories);
        }
      }
      return result;
    }
  }

  selectCategory(item) {
    this.category = this.capitalizeFirstLetter(item.Value);
    this.filterCtgs = [];
    this.subcategory = undefined;
  }

  selectSubcategory(item) {
    this.subcategory = this.capitalizeFirstLetter(item.Value);
    this.filterSubctgs = [];
  }

  capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }

  onClickedOutside(e: Event) {
    switch (e.srcElement.id) {
      case 'ctg':
        this.filterSubctgs = [];
        break;
      case 'subctg':
        this.filterCtgs = [];
        break;
      default:
        this.filterCtgs = [];
        this.filterSubctgs = [];
        break;
    }
  }

  searchWorks() {
    this.works = [];
    this.filterCtgs = [];
    this.filterSubctgs = [];
    this.spinner = true;
    this.distance = this.slider;
    this.togglePanel = false;
    this.ratingCmp = this.convertRatingType(this.ratingCompare);
    this.selSort = this.convertSortType(this.sort);   

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
    this.getWorksByAdvFilters(this.category, this.subcategory, date, timeZone,
           this.vendorName, this.ratingCmp, this.rating, this.reviewsChecked,
           this.latitude, this.longitude, this.distance,
           this.selCategories, this.selSubcategories, this.city, this.selSort);
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

  convertSortType (sort: string): number {
    switch (sort) {
      case 'rating':
        return 1;
      case 'name':
        return 2;
      case 'distance':
        return 3;
      default:
        return 1;
    }
  }

  getWorksByAdvFilters(category: string, subcategory: string, date: string, timeZone: number,
      vendor: string, ratingcompare: string, rating: number, reviews: boolean,
      latitude: number, longitude: number, distance: number,
      categories: string[], subcategories: string[], city: string, sort: number) {
        this.works = [];
    this.searchService.getWorksByAdvFilters(category, subcategory, date, timeZone,
    vendor, ratingcompare, rating, reviews, latitude, longitude, distance,
    categories, subcategories, city, sort)
    .then(works => {      
      this.works = works;
      this.pagedWorks = this.getWorksPage();
      this.searchMarkers = this.getMarkers();
      this.spinner = false;
      this.loaded = true;
      this.mapRedirect();
      this.ref.detectChanges();
    }).catch(err => this.spinner = false);
  }

  reset() {
    this.category = undefined;
    this.subcategory = undefined;
    this.date = undefined;
    this.resetAdvFilters();
    this.searchWorks();
  }

  resetAdvFilters() {
    this.vendorName = undefined;
    this.ratingCompare = 'greater';
    this.rating = undefined;
    this.place = undefined;
    this.latitude = undefined;
    this.longitude = undefined;
    this.slider = 0;
    this.distance = 0;
    this.reviewsChecked = false;
    this.selCategories = [];
    this.selSubcategories = [];
    this.city = '';
  }

  scrollToElement(id) {
    const element = document.querySelector('#' + id);
    element.scrollIntoView(false);
  }

  highlight(performer, id) {
    if (this.selMarker && this.selMarker.works.filter(e => e.PerformerType === performer && e.Id === id).length > 0) {
      return true;
    } else {
      return false;
    }
  }

  markerHandle(marker) {
    this.selMarker = marker;
    this.selected = marker.name;
    this.scrollToElement(this.selMarker.works[0].PerformerType + this.selMarker.works[0].Id);
  }

  initAdvancedFilters() {
    this.resetAdvFilters();
    this.categoryService.getAll()
    .then(resp => {
      this.categories = resp.body as Category[];
    });
    this.sort = 'rating';
  }

  isAnyFilterSelected() {
    if (this.vendorName === undefined &&
        this.ratingCompare === 'greater' &&
        this.rating === undefined &&
        this.place === undefined &&
        this.latitude === undefined &&
        this.longitude === undefined &&
        this.slider === 0 &&
        this.distance === 0 &&
        this.reviewsChecked === false &&
        this.selCategories.length === 0 &&
        this.selSubcategories.length === 0
      ) {
      return false;
    } else {
      return true;
    }
  }

  showSlider() {
    if (this.slider >= 100) {
      return this.slider.toString() + '+';
    } else {
      return this.slider.toString();
    }
  }

  categoriesChanged() {
    this.selSubcategories = [];
    this.subcategories = [];
    this.subcategories = getSubcategories(this.categories, this.selCategories);

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

  getSubcategories(categories, selected) {
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

  subcategoriesChanged() {
    // console.log(this.selSubcategories);
  }

  onMapReady(map: NguiMap) {
  }

  emptyWorks() {
    return !this.spinner && this.works.length === 0;
  }

  mapRedirect() {
    if (this.works && this.works.length > 0) {
      const loc = this.works[0].Location;
      this.center = new google.maps.LatLng(loc.Latitude, loc.Longitude);
      this.ref.detectChanges();
    }
  }

  selectWork(name: string, loc: LocationModel) {
    this.center = new google.maps.LatLng(loc.Latitude, loc.Longitude);
    this.selected = name;
    this.ref.detectChanges();
  }

  pageChanged(page) {
    this.selectedPage = page;
    this.pagedWorks = this.getWorksPage();
    this.searchMarkers = this.getMarkers();
  }

  getWorksPage(): SearchWork[] {
    return this.works.slice((this.selectedPage - 1) * Number(this.pageSize), this.selectedPage * Number(this.pageSize));
  }

  getPageSize() {
    return Number(this.pageSize);
  }

  getMarkers(): SearchMarker[] {
    let exist = false;
    let markers = [];
    const works = this.works.slice((this.selectedPage - 1) * Number(this.pageSize), this.selectedPage * Number(this.pageSize));
    for (let i = 0; i < works.length; i++) {
      exist = false;
      for (let j = 0; j < markers.length; j++) {
        if (works[i].PerformerName === markers[j].name) {
          markers[j].works.push(works[i]);
          exist = true;
        }
      }
      if (!exist) {
        const marker = {
          name: works[i].PerformerName,
          latitude: works[i].Location.Latitude,
          longitude: works[i].Location.Longitude,
          works: [works[i]]
        };
        markers.push(marker);
      }
    }
    return markers;
  }

  pageSizeChanged() {
    // check if sliced works are out of range
    if ((this.works.length - (this.selectedPage - 1) * Number(this.pageSize)) <= 0) {
      this.selectedPage = 1;
    }
    this.pageChanged(this.selectedPage);
  }

  initialized(autocomplete: any) {
    this.autocomplete = autocomplete;
  }

  placeChanged(event: any) {
    this.locationService.getLocDetails(event.geometry.location.lat(), event.geometry.location.lng())
      .subscribe(result => {
        this.city = result
          .address_components[result.address_components.findIndex(x => x.types.length === 2 && x.types.includes("locality") && x.types.includes("political"))]
          .short_name;
        this.longitude = event.geometry.location.lng();
        this.latitude = event.geometry.location.lat();

        this.searchWorks();
        this.ref.detectChanges();
      });
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

  initDarepicker() {
    /* datepicker settings */
    this.mode = 'date';           /* select day */
    this.firstDayOfWeek = '1';    /* start calendar from first day of week */
  }
}

