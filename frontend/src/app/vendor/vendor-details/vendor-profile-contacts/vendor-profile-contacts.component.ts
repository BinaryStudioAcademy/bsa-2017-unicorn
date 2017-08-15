import { Component, OnInit, Input, AfterViewChecked, ViewChild } from '@angular/core';

import { NguiMapModule, Marker } from "@ngui/map";

import { MapModel } from "../../../models/map.model";
import { Vendor } from '../../../models/vendor.model';

@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass'],
})
export class VendorProfileContactsComponent implements OnInit {
  @Input() private vendor: Vendor;

  map: MapModel= {
    center: {lat: this.vendor.location.lat, lng: this.vendor.location.lng},
    zoom: 18,    
    title: "Overcat 9000",
    label: "Overcat 9000",
    markerPos: {lat: this.vendor.location.lat, lng: this.vendor.location.lng}
  };

  constructor() { }

  ngOnInit() {
  }
}
