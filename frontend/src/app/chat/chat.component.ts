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

  ownerId: number;
  dialogs: DialogModel[];
  dialog: DialogModel;
  messages: MessageModel[];  
  selectedId: number;
  writtenMessage: string;  
  inputHeight: number = 43;  
  containerHeight = 300;
  noMessages: boolean = false; 
  needScroll: boolean = false;

  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    this.initialize().then(() => this.startScroll());   
  } 

  ngAfterViewChecked() {
    if (this.needScroll) {
      this.scrollMessages();
    }
  }

  initialize(){
    this.ownerId = +this.tokenHelper.getClaimByName('accountid');    
    return this.chatService.getDialogs(this.ownerId).then(res => {
      if(res !== undefined && res !== null){
        this.dialogs = res;
        if(this.dialogs !== null && this.dialogs.length !== 0){
          this.containerHeight = 300;
          this.selectedId = this.dialogs[0].Id;
          return this.getDialog();         
        }
        else{
          this.messages = [];
          this.containerHeight = 150;
        }
      }      
    });    
  }

  getDialog(){
    return this.chatService.getDialog(this.selectedId).then(res => {
      this.dialog = res;
      this.messages = this.dialog.Messages;      
    });    
  }

  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey){      
        this.onWrite();  
      } 
      else{ 
        this.changeTextareaSize();
      }
    }, 0);
  }

  onSelect(dialogId: number) {    
    this.selectedId = dialogId;
    this.getDialog().then(() => this.startScroll());   
  }

  onWrite(){  
    if(this.writtenMessage !== undefined){
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), "\n");
      str = str.replace((/\s{2,}/ig), " ");      
      if(str !== " " && str !== "\n"){
        this.writtenMessage = this.writtenMessage.trim();
        this.addMessage();
        this.writtenMessage = undefined;
      }
      else{
        this.writtenMessage = undefined;
      }
    } 
    this.normalTeaxareaSize();
  }   
  

  addMessage(){
    let message = {
      DialogId: this.selectedId,
      IsReaded: false, 
      OwnerId: this.ownerId,
      Message: this.writtenMessage, 
      Date: new Date(),
      isLoaded: true
    };
    this.messages.push(message);     
    this.scrollMessages();         
    this.chatService.addMessage(message).then(() =>  {
      this.messages.find(x => x.isLoaded).isLoaded = false;
    });
  }

  readNotReadedMessages(){
    let isChanged = false;
    this.messages.filter(x => !x.IsReaded).forEach(mes => {
      if(mes.OwnerId !== this.ownerId){
        mes.IsReaded = true;
        isChanged = true;
      }
    });
    if(isChanged){
      this.chatService.updateMessages(this.dialog.Id, this.ownerId);
    }
  }
    
  startScroll() {
    this.needScroll = true;
  }

  scrollMessages(){ 
    this.noMessages = true;
      if (this.messagesElement && this.messagesElement.nativeElement.scrollTop !== this.messagesElement.nativeElement.scrollHeight) {
        this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight; 
        this.needScroll = false; 
        this.noMessages = false;
      }
  }
  normalTeaxareaSize(){
    if(this.textarea !== undefined){ 
      this.textarea.nativeElement.style.height = 43 + 'px';     
    }
  }
  changeTextareaSize(){
    if(this.textarea !== undefined){ 
      this.textarea.nativeElement.style.height = 0 + 'px';
      var height = this.textarea.nativeElement.scrollHeight;      
      this.textarea.nativeElement.style.height = height + 2 + 'px';
    }
  }
}