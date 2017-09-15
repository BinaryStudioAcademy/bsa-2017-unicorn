import { Injectable } from '@angular/core';

import { DataService } from '../services/data.service';
import { BookOrder } from '../models/book/book-order';

@Injectable()
export class BookOrderService {

  constructor(private dataService: DataService) {
    this.dataService.setHeader('Content-Type', 'application/json');
  }

  public createOrder(book: BookOrder) {
    return this.dataService.postRequest('book/order', book);
  }
}
