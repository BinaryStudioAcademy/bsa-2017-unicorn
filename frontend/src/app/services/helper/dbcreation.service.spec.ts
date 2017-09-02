import { TestBed, inject } from '@angular/core/testing';

import { DbcreationService } from './dbcreation.service';

describe('DbcreationService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DbcreationService]
    });
  });

  it('should be created', inject([DbcreationService], (service: DbcreationService) => {
    expect(service).toBeTruthy();
  }));
});
