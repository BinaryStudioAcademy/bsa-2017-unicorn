import { Component, OnInit, Input } from '@angular/core';

import { NguiMapModule, Marker } from "@ngui/map";

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
  
  location: Location;
  map: MapModel;
  
  constructor(
    private locationService: LocationService, 
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    this.locationService.getById(this.vendor.LocationId)
      .then(resp => { 
        this.location = resp.body as Location;
        this.map = {
          center: {lat: this.location.Latitude, lng: this.location.Longitude},
          zoom: 18,    
          title: "Overcat 9000",
          label: "",
          markerPos: {lat: this.location.Latitude, lng: this.location.Longitude}
        };
      });
  }

}
