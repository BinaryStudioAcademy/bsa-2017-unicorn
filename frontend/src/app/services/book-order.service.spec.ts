import { TestBed, inject } from '@angular/core/testing';

import { BookOrderService } from './book-order.service';

describe('BookOrderService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BookOrderService]
    });
  });

  it('should be created', inject([BookOrderService], (service: BookOrderService) => {
    expect(service).toBeTruthy();
  }));
});
