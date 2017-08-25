import { Injectable } from '@angular/core';

import { DataService } from '../data.service';

import { BookCard } from '../../models/dashboard/book-card';

@Injectable()
export class DashboardService {

  constructor(private dataService: DataService) { }

  getVendorBooks(id: number, role: string): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${role}/${id}`);
  }

}
