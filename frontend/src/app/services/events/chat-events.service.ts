import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { DialogModel } from "../../models/chat/dialog.model";
import { MessageModel } from "../../models/chat/message.model";

@Injectable()
export class ChatEventsService {
  private _onChatOpen = new Subject<DialogModel>(); 
  private _onChatClose = new Subject<void>(); 
  private _onChatInit = new Subject<DialogModel>();

  private _onDialogCreateFromMiniChatToChat = new Subject<DialogModel>();

  private _onMessageCreateFromMiniChatToChat = new Subject<MessageModel>();
  private _onMessageCreateFromChatToMiniChat = new Subject<MessageModel>();

  private _onMessageReadFromMiniChatToChat = new Subject<number>();
  private _onMessageReadFromChatToMiniChat = new Subject<number>();

  private _onMessageDeleteFromChatToMiniChat = new Subject<MessageModel>();

  openChatEvent$ = this._onChatOpen.asObservable();  
  closeChatEvent$ = this._onChatClose.asObservable();  
  initChatEvent$ = this._onChatInit.asObservable();
  
  createDialogFromMiniChatToChatEvent$ = this._onDialogCreateFromMiniChatToChat.asObservable();

  createMessageFromChatToMiniChatEvent$ = this._onMessageCreateFromChatToMiniChat.asObservable();
  createMessageFromMiniChatToChatEvent$ = this._onMessageCreateFromMiniChatToChat.asObservable();

  readMessageFromChatToMiniChatEvent$ = this._onMessageReadFromChatToMiniChat.asObservable();
  readMessageFromMiniChatToChatEvent$ = this._onMessageReadFromMiniChatToChat.asObservable();

  deleteMessageFromChatToMiniChatEvent$ = this._onMessageDeleteFromChatToMiniChat.asObservable();

  openChat(dialog: DialogModel) {
    this._onChatOpen.next(dialog);
  }

  closechat() {
    this._onChatClose.next();
  }

  initChat(dialog: DialogModel){
    this._onChatInit.next(dialog);
  }

  dialogCreateFromMiniChatToChat(dialog: DialogModel){
    this._onDialogCreateFromMiniChatToChat.next(dialog);
  }

  messageCreateFromMiniChatToChat(mes: MessageModel){
    this._onMessageCreateFromMiniChatToChat.next(mes);
  }

  messageDeleteFromChatToMiniChat(mes: MessageModel)
  {
    this._onMessageDeleteFromChatToMiniChat.next(mes);
  }

  messageCreateFromChatToMiniChat(mes: MessageModel){
    this._onMessageCreateFromChatToMiniChat.next(mes);
  }

  messageReadFromMiniChatToChat(dialogId: number){
    this._onMessageReadFromMiniChatToChat.next(dialogId);
  }

  messageReadFromChatToMiniChat(dialogId: number){
    this._onMessageReadFromChatToMiniChat.next(dialogId);
  }

}