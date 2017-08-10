import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model';
import { PortfolioItem } from '../models/portfolio-item.model';

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

  getVendorPorfolio(vendorId: number) : PortfolioItem[] {
    var history: PortfolioItem[] = [
      {
        category: "Work category",
        workType: "Work type",
        image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
        rating: 4,
        historyId: 1,
        reviewId: 1
      },
      {
        category: "Work category",
        workType: "Work type",
        image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
        rating: 4,
        historyId: 1,
        reviewId: 1
      },
      {
        category: "Work category",
        workType: "Work type",
        image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
        rating: 4,
        historyId: 1,
        reviewId: 1
      },
      {
        category: "Work category",
        workType: "Work type",
        image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
        rating: 4,
        historyId: 1,
        reviewId: 1
      },
      {
        category: "Work category",
        workType: "Work type",
        image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
        rating: 4,
        historyId: 1,
        reviewId: 1
      }
    ];
      return history;
  }
}
