import { Injectable } from '@angular/core';

import { SearchWork } from '../models/search/search-work';

import { DataService } from './data.service';

@Injectable()
export class SearchService {

  constructor(private dataService: DataService) { }

  getWorksByBaseFilters(category: string, subcategory: string, date: string, timeZone: number): Promise<SearchWork[]> {
    if (category === undefined) { category = ''; }
    if (subcategory === undefined) { subcategory = ''; }
    if(!date) {date = '';} 
    const queryParams = `search?category=${category}&subcategory=${subcategory}&date=${date}&timeZone=${timeZone}`;
    return this.dataService.getRequest<SearchWork[]>(queryParams);
  }

  getWorksByAdvFilters(category: string, subcategory: string, date: string, timeZone: number,
  vendor: string, ratingcompare: string, rating: number, reviews: boolean,
  latitude: number, longitude: number, distance: number,
  categories: string[], subcategories: string[], city: string, sort: number): Promise<SearchWork[]> {

    let d: string; let rt: string; let rv: string;
    let lat: string; let long: string; let dist: string;
    let ctg: string; let subctg: string; let srt: string;

    let cityString: string;    
    if (category === undefined) { category = ''; }
    if (subcategory === undefined) { subcategory = ''; }
    if (!date) { date = ''; } 
    if (vendor === undefined) { vendor = ''; }
    if (ratingcompare === undefined) { ratingcompare = ''; }
    if (rating === undefined) { rt = ''; } else { rt = rating.toString(); }
    if (reviews === undefined) { rv = ''; } else { rv = reviews.toString(); }
    if (latitude === undefined) { lat = ''; } else { lat = latitude.toString(); }
    if (longitude === undefined) { long = ''; } else { long = longitude.toString(); }
    if (distance === 100) { distance = undefined; } // 100+ km feature
    if (distance === undefined) { dist = ''; } else { dist = distance.toString(); }
    if (city === undefined) { city = ''; }
    if (sort === undefined) { srt = ''; } else { srt = sort.toString(); }
    if (categories === undefined) {
      ctg = '&categories=';
    } else {
      ctg = '';
      categories.forEach(c => ctg += `&categories=${c}`);
    }
    if (subcategories === undefined) {
      subctg = '&subcategories=';
    } else {
      subctg = '';
      subcategories.forEach(s => subctg += `&subcategories=${s}`);
    }

    const queryParams = `search?category=${category}&subcategory=${subcategory}&date=${date}&timeZone=${timeZone}
    &vendor=${vendor}&ratingcompare=${ratingcompare}&rating=${rt}&reviews=${rv}
    &latitude=${lat}&longitude=${long}&distance=${dist}
    ${ctg}${subctg}&city=${city}&sort=${srt}`;    
    return this.dataService.getRequest<SearchWork[]>(queryParams);
  }
}
