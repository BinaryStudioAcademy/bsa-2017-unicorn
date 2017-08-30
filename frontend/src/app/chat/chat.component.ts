import { Component, OnInit, ViewChild } from '@angular/core';
import { DialogModel } from "../models/chat/dialog.model";
import { MessageModel } from "../models/chat/message.model";
import { NgClass } from '@angular/common';
import { ChatService } from "../services/chat/chat.service";
import { TokenHelperService } from "../services/helper/tokenhelper.service";

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

  ownerId: string;
  dialogs: DialogModel[];
  messages: MessageModel[];
  me: number;
  myParticipant: string;
  selectedId: number;
  writtenMessage: string;  
  inputHeight: number = 43;  
  containerHeight = 300;

  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    this.ownerId = this.tokenHelper.getClaimByName('accountid');
    this.chatService.getDialogs(+this.ownerId).then(res => {
      if(res !== undefined){
        this.dialogs = res;
        if(this.dialogs !== null && this.dialogs.length !== 0){
          this.containerHeight = 300;
          this.selectedId = this.dialogs[0].Id;
          this.messages = this.dialogs[0].Messages;
          this.me = this.dialogs[0].ParticipantOneId;
          this.myParticipant = this.dialogs[0].ParticipantName;
          //console.log(this.dialogs);
        }
        else{
          this.messages = null;
          this.containerHeight = 150;
        }
      }
    });
    // this.mockData();
    // this.selectedId = this.dialogs[0].Id;
    // this.messages = this.dialogs[0].Messages;
    // this.me = this.dialogs[0].FirstParticipant;
    // this.myParticipant = this.dialogs[0].SecondParticipant;
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
    this.me = this.dialogs.find(x => x.Id === dialogId).ParticipantOneId;
    this.myParticipant = this.dialogs.find(x => x.Id === dialogId).ParticipantName;
    setTimeout(() => {
      this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
    }, 0);
}

  onWrite(){  
    if(this.writtenMessage !== undefined){
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), "\n");
      str = str.replace((/\s{2,}/ig), " ");      
      if(str !== " " && str !== "\n"){
        this.addMessage();
      }
      else{
        this.writtenMessage = undefined;
      }
    } 
    this.textarea.nativeElement.style.height = 43 + 'px';     
  }   
  

  addMessage(){
    let message = {
      DialogId: this.selectedId,
      IsReaded: false, 
      OwnerId: this.me,
      Message: this.writtenMessage, 
      Date: new Date(),
      isLoaded: true
    };
    this.messages.push(message); 
    setTimeout(() => {
      this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
    }, 0);   
    this.chatService.addMessage(message).then(() =>  {
      this.messages.find(x => x.isLoaded).isLoaded = false;
    });
  }

  // mockData(){
  //   this.dialogs = [
  //     {
  //       Id: 1,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 2,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 3,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 4,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 5,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 6,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 7,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 8,
  //       FirstParticipant: "John",
  //       SecondParticipant: "Alex",        
  //       Messages: [{
  //         Id: 1,
  //         IsReaded: true, 
  //         Owner: "Alex",
  //         Message: "Hello. I need some help",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 2,
  //         IsReaded: true, 
  //         Owner: "John",          
  //         Message: "Hello. What is a problem?",
  //         Date: new Date(Date.now())
  //       }]
  //     },
  //     {
  //       Id: 9,
  //       FirstParticipant: "John",       
  //       SecondParticipant: "Anna",        
  //       Messages: [{
  //         Id: 3,
  //         IsReaded: true, 
  //         Owner: "John",
  //         Message: "What about payment?!!!",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 4,
  //         IsReaded: true, 
  //         Owner: "Anna",
  //         Message: "Oh, sorry, tomorrow will be",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 5,
  //         IsReaded: true, 
  //         Owner: "John",
  //         Message: "So, I am waiting",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 6,
  //         IsReaded: true, 
  //         Owner: "Anna",
  //         Message: "I have money, we need to meet up",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 5,
  //         IsReaded: true, 
  //         Owner: "John",
  //         Message: "So, I am waiting",
  //         Date: new Date(Date.now())
  //       },
  //       {
  //         Id: 6,
  //         IsReaded: true, 
  //         Owner: "Anna",
  //         Message: "I have money, we need to meet up",
  //         Date: new Date(Date.now())
  //       }]
  //     }
  //   ];    
  // }

}
