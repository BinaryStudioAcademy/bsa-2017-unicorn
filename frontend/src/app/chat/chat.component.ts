import { Component, OnInit } from '@angular/core';
import { DialogModel } from "../models/chat/dialog.model";
import { MessageModel } from "../models/chat/message.model";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.sass']
})
export class ChatComponent implements OnInit {
  dialogs: DialogModel[] = [];
  messages: MessageModel[] = [];
  me: string;
  myParticipant: string;
  selectedId: number;
  writtenMessage: string;
  constructor() { }

  ngOnInit() {
    this.mockData();
    this.selectedId = this.dialogs[0].Id;
    this.messages = this.dialogs[0].Messages;
    this.me = this.dialogs[0].FirstParticipant;
    this.myParticipant = this.dialogs[0].SecondParticipant;
  }

  onSelect(dialogId: number) {
    this.selectedId = dialogId;
    this.messages = this.dialogs.find(x => x.Id === dialogId).Messages;
    this.me = this.dialogs.find(x => x.Id === dialogId).FirstParticipant;
    this.myParticipant = this.dialogs.find(x => x.Id === dialogId).SecondParticipant;
}

  onWrite(){
    if(this.writtenMessage !== undefined && this.writtenMessage !== ''){
    this.messages.push(
      {
      Id: 3, 
      Sender: this.me, 
      Receiver: this.myParticipant, 
      Message: this.writtenMessage, 
      Date: new Date(Date.now())
    });
    this.writtenMessage = undefined;
  }
  }

  mockData(){
    this.dialogs = [
      {
        Id: 1,
        FirstParticipant: "John",
        FirstParticipantsId: 1,
        SecondParticipant: "Alex",
        SecondParticipantsId: 2,
        Messages: [{
          Id: 1,
          Sender: "Alex",
          Receiver: "John",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Sender: "John",
          Receiver: "Alex",
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 2,
        FirstParticipant: "John",
        FirstParticipantsId: 1,
        SecondParticipant: "Anna",
        SecondParticipantsId: 3,
        Messages: [{
          Id: 3,
          Sender: "John",
          Receiver: "Anna",
          Message: "What about payment?!!!",
          Date: new Date(Date.now())
        },
        {
          Id: 4,
          Sender: "Anna",
          Receiver: "John",
          Message: "Oh, sorry, tomorrow will be",
          Date: new Date(Date.now())
        },
        {
          Id: 5,
          Sender: "John",
          Receiver: "Anna",
          Message: "So, I am waiting",
          Date: new Date(Date.now())
        },
        {
          Id: 6,
          Sender: "Anna",
          Receiver: "John",
          Message: "I have money, we need to meet up",
          Date: new Date(Date.now())
        },
        {
          Id: 5,
          Sender: "John",
          Receiver: "Anna",
          Message: "So, I am waiting",
          Date: new Date(Date.now())
        },
        {
          Id: 6,
          Sender: "Anna",
          Receiver: "John",
          Message: "I have money, we need to meet up",
          Date: new Date(Date.now())
        }]
      }
    ];    
  }

}
