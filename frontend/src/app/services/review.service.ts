import { Injectable } from '@angular/core';

import { Review } from '../models/review.model';

@Injectable()
export class ReviewService {

  constructor() { }

  getVendorReviews(vendorId: number): Review[] {
    return [
      {
        Avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        Date: new Date(Date.now()),
        From: "Anton",
        To: "Name Surname",
        Grade: 4,
        Description: "Good, good"
      },
      {
        Avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        Date: new Date(Date.now()),
        From: "Egor",
        To: "Name Surname",
        Grade: 5,
        Description: "Very good"
      },
      {
        Avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        Date: new Date(Date.now()),
        From: "Kolya",
        To: "Name Surname",
        Grade: 5,
        Description: "Excellent"
      }
    ];
  }

  getCompanyReviews(companyId: number): Review[] {
    return [
      {
        Avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        Date: new Date(Date.now()),
        From: "Anton",
        To: "Name Surname",
        Grade: 4,
        Description: "Good, good"
      },
      {
        Avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        Date: new Date(Date.now()),
        From: "Egor",
        To: "Name Surname",
        Grade: 5,
        Description: "Very good"
      },
      {
        Avatar: "https://image.flaticon.com/icons/png/512/78/78373.png",
        Date: new Date(Date.now()),
        From: "Kolya",
        To: "Name Surname",
        Grade: 5,
        Description: "Excellent"
      }
    ];
  }

}
