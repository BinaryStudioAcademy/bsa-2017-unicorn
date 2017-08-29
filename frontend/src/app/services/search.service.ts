import { Injectable } from '@angular/core';

import { SearchWork } from '../models/search/search-work';

import { DataService } from './data.service';

@Injectable()
export class SearchService {

  constructor(private dataService: DataService) { }

  getWorksByBaseFilters(category: string, subcategory: string, date: number): Promise<SearchWork[]> {
    const query = `search?category=${category}&subcategory=${subcategory}&date=${date}`;
    return this.dataService.getRequest<SearchWork[]>(query);
  }

  getAllWorks(): Promise<SearchWork[]> {
    return this.dataService.getRequest<SearchWork[]>('search');
  }

}
