import { Injectable } from '@angular/core';

import { PopularCategory } from '../models/popular/PopularCategory';
import { Performer } from '../models/popular/Performer';

import { DataService } from './data.service';

@Injectable()
export class PopularService {

  constructor(private dataService: DataService) { }

  loadCategories(): Promise<PopularCategory[]> {
    return this.dataService.getRequest<PopularCategory[]>('popular/categories');
  }

  loadPopularPerformers(): Promise<Performer[]> {
    return this.dataService.getRequest<Performer[]>('popular/performers');
  }

  loadPerformers(id: number): Promise<Performer[]> {
    return this.dataService.getRequest<Performer[]>(`popular/performers/${id}`);
  }

}
