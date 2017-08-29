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
  isShiftBeforeEnter: boolean= false;

  inputHeight: number = 45;
  constructor() { }

  ngOnInit() {
    this.mockData();
    this.selectedId = this.dialogs[0].Id;
    this.messages = this.dialogs[0].Messages;
    this.me = this.dialogs[0].FirstParticipant;
    this.myParticipant = this.dialogs[0].SecondParticipant;
  }

  onChange(event){ 
    if(event.key === "Shift"){
      this.isShiftBeforeEnter = true;
    }    
    else if(event.key === "Enter" && (event.shiftKey || this.isShiftBeforeEnter)){      
      this.inputHeight += 20;
      this.isShiftBeforeEnter = false;      
    }      
    else if(event.key === "Enter"){
      if(this.writtenMessage !== undefined &&
        this.writtenMessage.match(/\w+|[a-z A-z А-Я а-я 0-9 іІЇї]|\.|\№|\+|\-|\,|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\;|\\|\/|\||\<|\>|\"|\'|\:|\?|\=/ig) !== null){      
          //event.preventDefault();
          this.onWrite();      
      }
      else{
        this.inputHeight = 45;   
        this.writtenMessage = undefined;
      }
    }
    else if(event.key === "Backspace" &&
      this.writtenMessage.match(/\w+|[a-z A-z А-Я а-я 0-9 іІЇї]|\.|\№|\+|\-|\,|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\;|\\|\/|\||\<|\>|\"|\'|\:|\?|\=/ig) === null){
      this.inputHeight -= 20; 
    }
    //console.log(event);
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
    //console.log(this.writtenMessage.match(/\w+|\.|\+|\-|\,|\!|\@|\#|\$|\%|\^|\&|\*|\(|\)|\;|\\|\/|\||\<|\>|\"|\'|\:|\?|\=/ig));
    this.inputHeight = 45;    
    this.messages.push({
      Id: 3, 
      Owner: this.me,
      Message: this.writtenMessage, 
      Date: new Date()
    });    
    this.writtenMessage = undefined;    
    setTimeout(() => {
      this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
    }, 0);
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
