import { Injectable } from '@angular/core';
import { DataService } from "../data.service";
import { DialogModel } from "../../models/chat/dialog.model";
import { MessageModel } from "../../models/chat/message.model";

@Injectable()
export class ChatService {

  constructor(private dataService: DataService) { 
    dataService.setHeader('Content-Type', 'application/json');
  }

  getDialogs(ownerId: number):Promise<DialogModel[]>{
    return this.dataService.getRequest<DialogModel[]>("chat/dialogs/" + ownerId);    
  }
  getDialog(dialogId: number):Promise<DialogModel>{
    return this.dataService.getRequest<DialogModel>("chat/" + dialogId);  
  }
  addDialog(dialog: DialogModel):Promise<DialogModel>{
    return this.dataService.postRequest("chat/dialog", dialog);  
  }


  addMessage(message: MessageModel):Promise<MessageModel>{
    return this.dataService.postRequest("chat/send", message);
  }

}
