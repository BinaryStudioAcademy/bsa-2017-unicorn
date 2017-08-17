import { Injectable } from '@angular/core';

import { Review } from '../models/review.model';
import { DataService } from "./data.service";

@Injectable()
export class ReviewService {

  constructor(private dataService: DataService) { }
}
