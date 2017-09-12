import { TestBed, inject } from '@angular/core/testing';

import { TaskMessagingService } from './task-messaging.service';

describe('TaskMessagingService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TaskMessagingService]
    });
  });

  it('should be created', inject([TaskMessagingService], (service: TaskMessagingService) => {
    expect(service).toBeTruthy();
  }));
});
