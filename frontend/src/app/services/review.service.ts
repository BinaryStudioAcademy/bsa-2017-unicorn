import { Injectable } from '@angular/core';

import { Review } from '../models/review.model';
import { DataService } from "./data.service";

@Injectable()
export class ReviewService {

  constructor(private dataService: DataService) { }

  // getVendorReviews(vendorId: number): Review[] {
  //   return [
  //     {
  //       avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
  //       date: new Date(Date.now()),
  //       from: "Anton",
  //       to: "Name Surname",
  //       grade: 4,
  //       description: "Good, good"
  //     },
  //     {
  //       avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
  //       date: new Date(Date.now()),
  //       from: "Egor",
  //       to: "Name Surname",
  //       grade: 5,
  //       description: "Very good"
  //     },
  //     {
  //       avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
  //       date: new Date(Date.now()),
  //       from: "Kolya",
  //       to: "Name Surname",
  //       grade: 5,
  //       description: "Excellent"
  //     }
  //   ];
  // }

  getCompanyReviews(companyId: number): Review[] {
    return [
      {
        avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        date: new Date(Date.now()),
        from: "Anton",
        to: "Name Surname",
        grade: 4,
        description: "Good, good"
      },
      {
        avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        date: new Date(Date.now()),
        from: "Egor",
        to: "Name Surname",
        grade: 5,
        description: "Very good"
      },
      {
        avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        date: new Date(Date.now()),
        from: "Kolya",
        to: "Name Surname",
        grade: 5,
        description: "Excellent"
      }
    ];
  }

}
