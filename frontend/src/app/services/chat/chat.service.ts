import { Injectable } from '@angular/core';
import { DataService } from "../data.service";
import { DialogModel } from "../../models/chat/dialog.model";
import { MessageModel } from "../../models/chat/message.model";
import { ChatFile } from "../../models/chat/chat-file";

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
  getDialogByOwner(dialogId: number, ownerId: number):Promise<DialogModel>{
    return this.dataService.getRequest<DialogModel>("chat/" + dialogId + "/" + ownerId);  
  }
  addDialog(dialog: DialogModel):Promise<DialogModel>{
    return this.dataService.postRequest("chat/dialog", dialog);  
  }
  findDialog(participantOneId: number, participantTwoId: number):Promise<DialogModel>{
    return this.dataService.getRequest("chat/dialog/find/" + participantOneId + "/" + participantTwoId);  
  }

  addMessage(message: MessageModel):Promise<MessageModel>{
    return this.dataService.postRequest("chat/send", message);
  }
  updateMessages(dialogId: number, ownerId: number):Promise<MessageModel>{
    return this.dataService.postRequest("chat/messages/update/"+ dialogId, ownerId);
  }

  uploadFiles(formDataFiles: any): Promise<ChatFile[]> {
    this.dataService.deleteHeader('Content-Type');    
    return this.dataService.postFullRequest("chat/upload", formDataFiles);
  }

}
