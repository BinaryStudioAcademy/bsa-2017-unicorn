import { Component, OnInit, Input, AfterViewChecked, ViewChild } from '@angular/core';

import { AgmMap, AgmMarker } from "@agm/core";

import { Vendor } from '../../models/vendor.model';

@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass'],
})
export class VendorProfileContactsComponent implements OnInit, AfterViewChecked {
  @Input() private vendorId: number;
  @ViewChild(AgmMap) private map: AgmMap;

  lat: number = 49.85711;
  lng: number = 24.01980; 
  markerTitle: string = 'Title';

  constructor() { }

  ngAfterViewChecked() {
    this.map.triggerResize()
      .then(() => {
        this.map.latitude = this.lat;
        this.map.longitude = this.lng;
      });
  }

  ngOnInit() {
  }
}
