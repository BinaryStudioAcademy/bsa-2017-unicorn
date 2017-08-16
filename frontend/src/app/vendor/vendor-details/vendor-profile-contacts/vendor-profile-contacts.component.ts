import { Component, OnInit, Input, AfterViewChecked, ViewChild } from '@angular/core';

import { NguiMapModule, Marker } from "@ngui/map";

import { MapModel } from "../../../models/map.model";
import { Vendor } from '../../../models/vendor.model';
import { Contact } from "../../../models/contact.model";
import { Location } from "../../../models/location.model";
import { LocationService } from "../../../services/location.service";
import { VendorService } from "../../../services/vendor.service";

@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass'],
})
export class VendorProfileContactsComponent implements OnInit {
  @Input() private vendorId: number;
  @Input() private locationId: number;
  contacts: Contact[];
  location: Location;
  map: MapModel;

  constructor(
    private locationService: LocationService, 
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    this.vendorService.getContacts(this.vendorId)
      .then(contacts => this.contacts = contacts);
    this.locationService.getById(this.locationId)
      .then(location => { 
        this.location = location;
        this.map = {
          center: {lat: this.location.Latitude, lng: this.location.Longitude},
          zoom: 18,    
          title: "Overcat 9000",
          label: "Overcat 9000",
          markerPos: {lat: this.location.Latitude, lng: this.location.Longitude}
        };
      });
  }
}
