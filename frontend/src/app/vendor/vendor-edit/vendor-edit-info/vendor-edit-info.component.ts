import { Component, OnInit, Input } from '@angular/core';

import { NguiMapModule, Marker } from "@ngui/map";
import {SuiModule} from 'ng2-semantic-ui';

import { Location } from "../../../models/location.model"
import { Vendor } from "../../../models/vendor.model";
import { MapModel } from "../../../models/map.model";

import { VendorService } from "../../../services/vendor.service";
import { LocationService } from "../../../services/location.service";

@Component({
  selector: 'app-vendor-edit-info',
  templateUrl: './vendor-edit-info.component.html',
  styleUrls: ['./vendor-edit-info.component.sass']
})
export class VendorEditInfoComponent implements OnInit {
  @Input() vendor: Vendor;
  
  birthday: Date;
  location: Location;
  map: MapModel;
  dataLoaded: boolean;
  
  constructor(
    private locationService: LocationService, 
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    this.dataLoaded = true;
    this.locationService.getById(this.vendor.LocationId)
      .then(resp => this.location = resp.body as Location)
      .then(() => this.map = {
          center: {lat: this.location.Latitude, lng: this.location.Longitude},
          zoom: 18,    
          title: "Overcat 9000",
          label: "",
          markerPos: {lat: this.location.Latitude, lng: this.location.Longitude}
        })
        .then(() => this.birthday = this.vendor.Birthday);
  }

  onDateSelected(date: Date): void {
    console.log(date.getDate());
  }

  saveVendor(): void {
    this.dataLoaded = false;
    this.vendor.Birthday = this.birthday;
    this.vendorService.updateVendor(this.vendor)
      .then(resp => this.vendor = resp.body as Vendor)
      .then(() => this.dataLoaded = true);
  }

}
