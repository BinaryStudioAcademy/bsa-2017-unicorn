import { TestBed, inject } from '@angular/core/testing';

import { PerformerService } from './performer.service';

describe('PerformerService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PerformerService]
    });
  });

  it('should be created', inject([PerformerService], (service: PerformerService) => {
    expect(service).toBeTruthy();
  }));
});
