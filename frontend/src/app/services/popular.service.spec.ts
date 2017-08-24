import { TestBed, inject } from '@angular/core/testing';

import { PopularService } from './popular.service';

describe('PopularService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PopularService]
    });
  });

  it('should be created', inject([PopularService], (service: PopularService) => {
    expect(service).toBeTruthy();
  }));
});
