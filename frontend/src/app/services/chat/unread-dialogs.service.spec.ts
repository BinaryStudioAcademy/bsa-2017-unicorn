import { TestBed, inject } from '@angular/core/testing';

import { UnreadDialogsService } from './unread-dialogs.service';

describe('UnreadDialogsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UnreadDialogsService]
    });
  });

  it('should be created', inject([UnreadDialogsService], (service: UnreadDialogsService) => {
    expect(service).toBeTruthy();
  }));
});
