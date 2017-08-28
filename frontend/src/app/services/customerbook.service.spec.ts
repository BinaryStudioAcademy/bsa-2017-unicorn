import { TestBed, inject } from '@angular/core/testing';

import { CustomerbookService } from './customerbook.service';

describe('CustomerbookService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CustomerbookService]
    });
  });

  it('should be created', inject([CustomerbookService], (service: CustomerbookService) => {
    expect(service).toBeTruthy();
  }));
});
