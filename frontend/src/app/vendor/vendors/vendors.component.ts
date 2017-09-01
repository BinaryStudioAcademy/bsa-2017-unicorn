import { Component, OnInit, ChangeDetectorRef } from '@angular/core';

import { NguiMapModule, Marker, NguiMap} from "@ngui/map";

import { Performer } from '../../models/performer.model';
import { LocationModel } from '../../models/location.model';

import { PerformerService } from '../../services/performer.service';

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.sass']
})
export class VendorsComponent implements OnInit {

  loaded: boolean;
  searchLoading: boolean

  /* pagination */
  pageSize = 10;
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

  constructor(
    private performerService: PerformerService,
    private ref: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.performerService.getAllPerformers().then(resp => {
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
    return this.performers.slice((this.selectedPage - 1) * this.pageSize, this.selectedPage * this.pageSize);
  }

  search() {
    this.searchLoading = true;
    return this.performerService.getPerformersByFilters(this.city, this.name)
      .then(resp => {
        this.performers = resp;
        this.searchLoading = false;
        this.mapRedirect();
        this.ref.detectChanges();
      }).catch(err => this.searchLoading = false);
  }

  reset() {
    this.city = '';
    this.name = '';
  }

  initialized(autocomplete: any) {
    this.autocomplete = autocomplete;
  }

  placeChanged(event) {
   // let place = this.autocomplete.getPlace();
    this.search();
    //this.center = place.geometry.location;
    // this.search();
    // for (let i = 0; i < place.address_components.length; i++) {
    //   let addressType = place.address_components[i].types[0];
    //   this.address[addressType] = place.address_components[i].long_name;
    // }
    this.ref.detectChanges();
  }

}
