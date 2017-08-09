import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model'

@Injectable()
export class VendorService {

  constructor(private dataService: DataService) { }

  getAllVendors() : Vendor[] {
    return;
  }

  getVendor(id: number) : Vendor {
    var vendor: Vendor = new Vendor();

    vendor.id = 0;
    vendor.firstName = "Name";
    vendor.lastName = "Surname";
    vendor.experience = 1;
    vendor.location = "Kyiv";
    vendor.rang = "Middle";
    vendor.avatarUrl = "https://image.flaticon.com/icons/png/512/78/78373.png";
    vendor.rating = 4;
    vendor.features = ["Excellent service", "We are going fast", "We've scratched cats since 1997", "Warm hands"];

    return vendor;
  }

  getVendorRating(vendorId: number) : number {
    return 95;
  }
}
