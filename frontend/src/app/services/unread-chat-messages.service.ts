import { Injectable } from '@angular/core';
import { DataService } from './data.service';

@Injectable()
export class UnreadChatMessagesService {

  constructor(private dataService: DataService) { }

  public getCountUnreadMessages(profileId: number) {
    return this.dataService.getRequest<number>("chat/unreadmessages/" + profileId);    
  }

}
