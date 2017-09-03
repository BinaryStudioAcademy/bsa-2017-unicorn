import { Injectable } from '@angular/core';
import { DataService } from './data.service';

import { CustomerBook } from '../models/book/book.model';

@Injectable()
export class CustomerbookService {

  constructor(
    private dataService: DataService
  ) { }

  getCustomerBooks(id: number): Promise<CustomerBook[]> {
    return this.dataService.getRequest<CustomerBook[]>(`book/customer/${id}`);
  }

  deleteBook(book: CustomerBook): Promise<any> {
    return this.dataService.deleteRequest(`book/${book.Id}`);
  }
}
