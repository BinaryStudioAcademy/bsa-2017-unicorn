import { Injectable } from '@angular/core';

import { Performer } from '../models/performer.model';

import { DataService } from './data.service';

@Injectable()
export class PerformerService {

  constructor(
    private dataService: DataService
  ) { }

  getAllPerformers(): Promise<Performer[]> {
    return this.dataService.getRequest('popular/allperformers');
  }

  getPerformersByFilters(city: string, name: string, role: string, minRating: number, withReviews: boolean, categories: number[]): Promise<Performer[]> {
    let categoriesString = categories ? categories.join('+') : '';
    let uriParams = `role=${role ? role : ''}
      &city=${city ? city : ''}
      &name=${name ? name : ''}
      &minRating=${minRating ? minRating : 0}
      &withReviews=${withReviews ? withReviews : false}
      &categoriesString=${categoriesString}`;
    return this.dataService.getRequest(`popular/search?${uriParams}`);
  }

}
