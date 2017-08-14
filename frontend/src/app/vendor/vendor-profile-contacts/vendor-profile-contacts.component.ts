import { Component, OnInit, Input, AfterViewChecked, ViewChild } from '@angular/core';

import { NguiMapModule, Marker } from "@ngui/map";

import { MapModel } from "../../models/map.model";
import { Vendor } from '../../models/vendor.model';

@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass'],
})
export class VendorProfileContactsComponent implements OnInit {
  @Input() private vendorId: number;

  map: MapModel= {
    center: {lat: 49.85711, lng: 24.01980},
    zoom: 18,    
    title: "Overcat 9000",
    label: "Overcat 9000",
    markerPos: {lat: 49.85711, lng: 24.01980}
  };

  constructor() { }

  ngOnInit() {
  }
}
