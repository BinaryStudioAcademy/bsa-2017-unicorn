import { Component, OnInit, ViewChild } from '@angular/core';
import { DialogModel } from "../models/chat/dialog.model";
import { MessageModel } from "../models/chat/message.model";
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.sass']
})
export class ChatComponent implements OnInit {
  @ViewChild('messagesBlock')
  private messagesElement: any;

  @ViewChild('textArea')
  private textarea: any;

  dialogs: DialogModel[] = [];
  messages: MessageModel[] = [];
  me: string;
  myParticipant: string;
  selectedId: number;
  writtenMessage: string;
  isShiftBeforeEnter: boolean= false;
  inputHeight: number = 43;

  constructor() { }

  ngOnInit() {
    this.mockData();
    this.selectedId = this.dialogs[0].Id;
    this.messages = this.dialogs[0].Messages;
    this.me = this.dialogs[0].FirstParticipant;
    this.myParticipant = this.dialogs[0].SecondParticipant;
  }

  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey){      
        this.onWrite();  
      } 
      else{  
          this.textarea.nativeElement.style.height = 0 + 'px';
          var height = this.textarea.nativeElement.scrollHeight;      
          this.textarea.nativeElement.style.height = height + 2 + 'px';
      }
    }, 0);
  }

  onSelect(dialogId: number) {
    this.selectedId = dialogId;
    this.messages = this.dialogs.find(x => x.Id === dialogId).Messages;
    this.me = this.dialogs.find(x => x.Id === dialogId).FirstParticipant;
    this.myParticipant = this.dialogs.find(x => x.Id === dialogId).SecondParticipant;
    setTimeout(() => {
      this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
    }, 0);
}

  onWrite(){  
    if(this.writtenMessage !== undefined){
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), " ");
      str = str.replace((/\s{2,}/ig), " ");      
      if(str !== " " && str !== "  " && str !== "\n"){
          this.messages.push({
            Id: 3,
            IsReaded: false, 
            Owner: this.me,
            Message: this.writtenMessage, 
            Date: new Date()
          });    
          this.writtenMessage = undefined;    
          setTimeout(() => {
            this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
          }, 0);
      }
      else{
        this.writtenMessage = undefined;
      }
    } 
    this.textarea.nativeElement.style.height = 43 + 'px';     
  }   
  

  mockData(){
    this.dialogs = [
      {
        Id: 1,
        FirstParticipant: "John",
        SecondParticipant: "Alex",        
        Messages: [{
          Id: 1,
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "Alex",
          Message: "Hello. I need some help",
          Date: new Date(Date.now())
        },
        {
          Id: 2,
          IsReaded: true, 
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
          IsReaded: true, 
          Owner: "John",
          Message: "What about payment?!!!",
          Date: new Date(Date.now())
        },
        {
          Id: 4,
          IsReaded: true, 
          Owner: "Anna",
          Message: "Oh, sorry, tomorrow will be",
          Date: new Date(Date.now())
        },
        {
          Id: 5,
          IsReaded: true, 
          Owner: "John",
          Message: "So, I am waiting",
          Date: new Date(Date.now())
        },
        {
          Id: 6,
          IsReaded: true, 
          Owner: "Anna",
          Message: "I have money, we need to meet up",
          Date: new Date(Date.now())
        },
        {
          Id: 5,
          IsReaded: true, 
          Owner: "John",
          Message: "So, I am waiting",
          Date: new Date(Date.now())
        },
        {
          Id: 6,
          IsReaded: true, 
          Owner: "Anna",
          Message: "I have money, we need to meet up",
          Date: new Date(Date.now())
        }]
      }
    ];    
  }

}
