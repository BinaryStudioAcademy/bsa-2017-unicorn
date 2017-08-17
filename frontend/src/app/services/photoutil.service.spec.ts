import { TestBed, inject } from '@angular/core/testing';

import { PhotoutilService } from './photoutil.service';

describe('PhotoutilService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PhotoutilService]
    });
  });

  it('should be created', inject([PhotoutilService], (service: PhotoutilService) => {
    expect(service).toBeTruthy();
  }));
});
