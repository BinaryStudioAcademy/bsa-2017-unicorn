import { TestBed, inject } from '@angular/core/testing';

import { DashMessagingService } from './dash-messaging.service';

describe('DashMessagingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DashMessagingService]
    });
  });

  it('should be created', inject([DashMessagingService], (service: DashMessagingService) => {
    expect(service).toBeTruthy();
  }));
});
