import { Component, OnInit, ViewChild } from '@angular/core';
import { DialogModel } from "../models/chat/dialog.model";
import { MessageModel } from "../models/chat/message.model";

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.sass']
})
export class ChatComponent implements OnInit {
@ViewChild('messagesBlock')
  private messagesElement: any;

  dialogs: DialogModel[] = [];
  messages: MessageModel[] = [];
  me: string;
  myParticipant: string;
  selectedId: number;
  writtenMessage: string;


  inputHeight: number = 40;
  maxInputHeight: number = 75;
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

  keyEvent(){    
    if(this.inputHeight < this.maxInputHeight){
      this.inputHeight += 15;      
    }
  }

  onWrite(){
    console.log(this.writtenMessage);
    this.inputHeight = 40;
    if(this.writtenMessage !== undefined && this.writtenMessage !== '' && this.writtenMessage != '\n'){
    this.messages.push(
      {
      Id: 3, 
      Owner: this.me,
      Message: this.writtenMessage, 
      Date: new Date(Date.now())
    });    
    this.writtenMessage = undefined;
    //this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
  }
  }

  mockData(){
    this.dialogs = [
      {
        Id: 1,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 2,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 3,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 4,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 5,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 6,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 7,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 8,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          Owner: "John",          
          Message: "Hello. What is a problem?",
          Date: new Date(Date.now())
        }]
      },
      {
        Id: 9,
        FirstParticipant: "John",       
        SecondParticipant: "Anna",        
        Messages: [{
          Id: 3,
          Owner: "John",
          Message: "What about payment?!!!",
          Date: new Date(Date.now())
        },
        {
          Id: 4,
          Owner: "Anna",
          Message: "Oh, sorry, tomorrow will be",
          Date: new Date(Date.now())
        },
        {
          Id: 5,
          Owner: "John",
          Message: "So, I am waiting",
          Date: new Date(Date.now())
        },
        {
          Id: 6,
          Owner: "Anna",
          Message: "I have money, we need to meet up",
          Date: new Date(Date.now())
        },
        {
          Id: 5,
          Owner: "John",
          Message: "So, I am waiting",
          Date: new Date(Date.now())
        },
        {
          Id: 6,
          Owner: "Anna",
          Message: "I have money, we need to meet up",
          Date: new Date(Date.now())
        }]
      }
    ];    
  }

}
