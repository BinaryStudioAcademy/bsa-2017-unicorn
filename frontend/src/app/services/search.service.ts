import { Injectable } from '@angular/core';

import { SearchPerformer } from '../models/search/search-performer';

import { DataService } from './data.service';

@Injectable()
export class SearchService {

  constructor(private dataService: DataService) { }

  getPerformersByBaseFilters(category: string, subcategory: string, date: number): Promise<SearchPerformer[]> {
    const query = `search/performers?category=${category}&subcategory=${subcategory}&date=${date}`;
    return this.dataService.getRequest<SearchPerformer[]>(query);
  }

  getAllPerformers(): Promise<SearchPerformer[]> {
    return this.dataService.getRequest<SearchPerformer[]>('search/performers');
  }

}
