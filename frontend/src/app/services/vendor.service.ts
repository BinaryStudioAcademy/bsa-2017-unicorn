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
    vendor.fio = "Name Surname";
    vendor.location = "Kyiv";
    vendor.rang = "Middle .Net developer";
    vendor.avatarUrl = "https://image.flaticon.com/icons/png/512/78/78373.png";
    vendor.workLetter = "My name is Randy Patterson, and I’m currently looking for a job in youth services. I have 10 years of experience working with youth agencies. I have a bachelor’s degree in outdoor education. I raise money, train leaders, and organize units. I have raised over $100,000 each of the last six years. I consider myself a good public speaker, and I have a good sense of humor.";
    vendor.rating = 4;
    vendor.reviewsCount = 3;
    vendor.features = ["Excellent service", "We are going fast", "We've scratched cats since 1997", "Warm hands"];

    return vendor;
  }
}
