import { Injectable } from '@angular/core';

import { Review } from '../models/review.model';
import { DataService } from "./data.service";

import { ShortReview } from '../models/short-review';

@Injectable()
export class ReviewService {

  constructor(private dataService: DataService) { }

  saveReview(review: ShortReview): Promise<any> {
    return this.dataService.postRequest('review', review);
  }
}
