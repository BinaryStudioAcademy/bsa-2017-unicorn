import { Injectable } from '@angular/core';

import { SearchPerformer } from '../models/search/search-performer';

import { DataService } from './data.service';

@Injectable()
export class SearchService {

  constructor(private dataService: DataService) { }

  getSearchPerformers(): Promise<SearchPerformer[]> {
    return this.dataService.getRequest<SearchPerformer[]>('search/performers');
  }

}
