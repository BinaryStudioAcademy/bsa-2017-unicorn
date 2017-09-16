import { Injectable } from '@angular/core';
import { DataService } from "../data.service";

@Injectable()
export class UnreadDialogsService {  

  constructor(private dataService: DataService) { }

  public getUnreadDialogsCount(accountId: number){
    return this.dataService.getRequest<number>('chat/unread/' + accountId);
  }

}
