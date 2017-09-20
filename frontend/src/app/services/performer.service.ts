import { Injectable } from "@angular/core";

import { Performer } from "../models/performer.model";
import { PerformersPage } from "../models/popular/performers-page.model";

import { DataService } from "./data.service";

@Injectable()
export class PerformerService {

  constructor(
    private dataService: DataService
  ) { }

  getAllPerformers(): Promise<Performer[]> {
    return this.dataService.getRequest('popular/allperformers');
  }

  getPerformersByFilters(
    city: string, name: string, role: string, rating: number, ratingCondition: string,
    withReviews: boolean, categories: number[], subcategories: number[], page: number, pageSize: number,
    latitude: number, longitude: number, distance: number, sort: string, date: string, timeZone: number
  ): Promise<PerformersPage> {
    let uriParams: string[] = [];

    if (categories)
      uriParams.push(`categoriesString=${categories.join('+')}`);

    if (subcategories)
      uriParams.push(`subcategoriesString=${subcategories.join('+')}`);

    if (city && city !== '') 
      uriParams.push(`city=${city}`);

    if (name && name !== '') 
      uriParams.push(`name=${name}`);

    if (role && role !== '') 
      uriParams.push(`role=${role}`);

    if (withReviews) 
      uriParams.push(`withReviews=${withReviews}`);

    if (rating && rating > 0)
      uriParams.push(`rating=${rating}`);

    if (ratingCondition && ratingCondition !== '')
      uriParams.push(`ratingCondition=${ratingCondition}`);

    if (latitude && longitude) {
      uriParams.push(`latitude=${latitude}`);
      uriParams.push(`longitude=${longitude}`);
    }

    if (distance && distance >= 0)
      uriParams.push(`distance=${distance}`);

    if (page && page > 0)
      uriParams.push(`page=${page}`);

    if (pageSize && pageSize > 0)
      uriParams.push(`pageSize=${pageSize}`);

    if (sort && sort !== '')
      uriParams.push(`sort=${sort}`);

    if(!date){
      date = '';
    }    
      uriParams.push(`date=${date}&timeZone=${timeZone}`);
    
    return this.dataService.getRequest(`popular/search?${uriParams.join('&')}`);
  }

}
