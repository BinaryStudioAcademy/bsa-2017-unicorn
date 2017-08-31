import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { DialogModel } from "../../models/chat/dialog.model";

@Injectable()
export class ChatEventsService {
  private _onChatOpen = new Subject<DialogModel>(); 
  private _onChatClose = new Subject<void>(); 

  openChatEvent$ = this._onChatOpen.asObservable();  
  closeChatEvent$ = this._onChatClose.asObservable();  

  openChat(dialog: DialogModel) {
    this._onChatOpen.next(dialog);
  }

  closechat() {
    this._onChatClose.next();
  }

}