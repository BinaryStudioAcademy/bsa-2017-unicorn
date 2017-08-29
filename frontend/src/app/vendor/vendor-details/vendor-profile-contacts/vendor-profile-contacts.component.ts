import { Component, OnInit, Input, AfterViewChecked, ViewChild } from '@angular/core';

import { NguiMapModule, Marker } from "@ngui/map";

import { MapModel } from "../../../models/map.model";
import { Vendor } from '../../../models/vendor.model';
import { Contact } from "../../../models/contact.model";
import { LocationModel } from "../../../models/location.model";
import { LocationService } from "../../../services/location.service";
import { VendorService } from "../../../services/vendor.service";

@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass'],
})
export class VendorProfileContactsComponent implements OnInit {
  @Input() private vendorId: number;
  
  contacts: Contact[];
  location: LocationModel;
  map: MapModel;
  vendor: Vendor;

  constructor(
    private locationService: LocationService, 
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    
    this.vendorService.getVendor(this.vendorId)
    .then(resp => 
      { 
        this.vendor = resp.body as Vendor;
        this.map = {
          center: {lat: this.vendor.Location.Latitude, lng: this.vendor.Location.Longitude},
          zoom: 18,    
          title: this.vendor.Name,
          label: this.vendor.Name,
          markerPos: {lat: this.vendor.Location.Latitude, lng: this.vendor.Location.Longitude}    
        };   
      });
    this.vendorService.getContacts(this.vendorId)
      .then(resp => this.contacts = resp.body as Contact[])
     
  }
}
