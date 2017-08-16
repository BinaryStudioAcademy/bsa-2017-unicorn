import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model';
import { PortfolioItem } from '../models/portfolio-item.model';

@Injectable()
export class VendorService {
  private resourceUrl: string;

  constructor(private dataService: DataService) 
  { 
    this.resourceUrl = dataService.buildUrl("vendors");
  }

  getAllVendors() : Promise<Vendor[]> {
    return this.dataService.getRequest<Vendor[]>(this.resourceUrl);
  }

  getVendor(id: number) : Promise<any> {
    return this.dataService.getRequest<any>(`${this.resourceUrl}/${id}`);

    // var vendor: Vendor = {
    // id: 0,
    // fio: "Name Surname",
    // location: "Kyiv",
    // rang: "Middle .Net developer",
    // avatarUrl: "https://image.flaticon.com/icons/png/512/78/78373.png",
    // workLetter: "My name is Randy Patterson, and I’m currently looking for a job in youth services. I have 10 years of experience working with youth agencies. I have a bachelor’s degree in outdoor education. I raise money, train leaders, and organize units. I have raised over $100,000 each of the last six years. I consider myself a good public speaker, and I have a good sense of humor.",
    // rating: 4,
    // reviewsCount: 3,
    // features: ["Excellent service", "We are going fast", "We've scratched cats since 1997", "Warm hands"],
    // workList: null
    // }
    // return vendor;
  }

  getRating(id: number): Promise<any> {
    return this.dataService.getRequest<any>(`${this.resourceUrl}/${id}/rating`);
  }

  getVendorPorfolio(vendorId: number): Promise<any> {
    return this.dataService.getRequest<any>(`${this.resourceUrl}/${vendorId}/portfolio`);
  }

  // getVendorPorfolio(vendorId: number) : PortfolioItem[] {
  //   var history: PortfolioItem[] = [
  //     {
  //       category: "Work category",
  //       workType: "Work type",
  //       image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
  //       rating: 4,
  //       historyId: 1,
  //       reviewId: 1
  //     },
  //     {
  //       category: "Work category",
  //       workType: "Work type",
  //       image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
  //       rating: 4,
  //       historyId: 1,
  //       reviewId: 1
  //     },
  //     {
  //       category: "Work category",
  //       workType: "Work type",
  //       image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
  //       rating: 4,
  //       historyId: 1,
  //       reviewId: 1
  //     },
  //     {
  //       category: "Work category",
  //       workType: "Work type",
  //       image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
  //       rating: 4,
  //       historyId: 1,
  //       reviewId: 1
  //     },
  //     {
  //       category: "Work category",
  //       workType: "Work type",
  //       image: "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
  //       rating: 4,
  //       historyId: 1,
  //       reviewId: 1
  //     }
  //   ];
  //     return history;
  // }
}
