import { TestBed, inject } from '@angular/core/testing';

import { TokenhelperService } from './tokenhelper.service';

describe('TokenhelperService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TokenhelperService]
    });
  });

  it('should be created', inject([TokenhelperService], (service: TokenhelperService) => {
    expect(service).toBeTruthy();
  }));
});
