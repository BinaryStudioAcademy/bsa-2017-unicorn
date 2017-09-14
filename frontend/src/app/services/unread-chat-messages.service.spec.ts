import { TestBed, inject } from '@angular/core/testing';

import { UnreadChatMessagesService } from './unread-chat-messages.service';

describe('UnreadChatMessagesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UnreadChatMessagesService]
    });
  });

  it('should be created', inject([UnreadChatMessagesService], (service: UnreadChatMessagesService) => {
    expect(service).toBeTruthy();
  }));
});
