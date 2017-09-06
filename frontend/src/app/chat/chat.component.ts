import { Component, OnInit, ViewChild, ChangeDetectorRef } from '@angular/core';
import { DialogModel } from "../models/chat/dialog.model";
import { MessageModel } from "../models/chat/message.model";
import { NgClass } from '@angular/common';
import { ChatService } from "../services/chat/chat.service";
import { TokenHelperService } from "../services/helper/tokenhelper.service";
import { NotificationService } from "../services/notifications/notification.service";
import { Subscription } from "rxjs/Subscription";
import { ChatEventsService } from "../services/events/chat-events.service";

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
  noMessages: boolean = true; 
  needScroll: boolean = false;

  dialogCreate: Subscription;
  messageCreate: Subscription;
  messageRead: Subscription;

  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService,
    private cdr: ChangeDetectorRef,
    private notificationService: NotificationService,
    private chatEventsService: ChatEventsService) { }

  ngOnInit() {
    this.getDialogs().then(() => this.startScroll());      
    this.notificationService.listen<any>("RefreshMessages", res => {
      this.getMessage(res);
    }); 
    this.notificationService.listen<any>("ReadNotReadedMessages", () => {
      this.messagesWereReaded();
    }); 

    this.dialogCreate = this.chatEventsService.createDialogFromMiniChatToChatEvent$.subscribe(dialog => {
      this.dialogs.push(this.checkLastMessage(dialog, dialog.Messages[dialog.Messages.length - 1].OwnerId));
    })

    this.messageCreate = this.chatEventsService.createMessageFromMiniChatToChatEvent$.subscribe(mes => {      
      if(this.dialogs.find(x => x.Id === mes.DialogId)){
        if(this.dialog.Id === mes.DialogId){
          this.messages.push(mes);     
          this.startScroll();   
        }
      }
    });

    this.messageRead = this.chatEventsService.readMessageFromMiniChatToChatEvent$.subscribe(dialogId => {
      if(this.dialogs.find(x => x.Id === dialogId)){
        if(this.dialog.Id === dialogId){
          this.readNotReadedMessages(this.dialog);
          if(!this.dialog.IsReadedLastMessage){
            this.dialog.IsReadedLastMessage= true;
          }
        }
        else{
          this.dialogs.find(x => x.Id === dialogId).IsReadedLastMessage = true;
        }
      }
    });
  } 

  ngAfterViewChecked() {
    if (this.needScroll) {      
      this.scrollMessages();
    }
  }  

  getDialogs(){
    this.ownerId = +this.tokenHelper.getClaimByName('accountid');
    return this.chatService.getDialogs(this.ownerId).then(res => {      
      if(res !== undefined){
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

  //get message, if anybody sent one to us
  getMessage(mes: MessageModel){
    if(!this.dialogs){
      this.getDialogs().then(() => {       
        this.startScroll();   
      });
    }
    else{
      let dialog = this.dialogs.find(x => x.Id === mes.DialogId);
      if(dialog && this.selectedId === dialog.Id){
        this.messages.push(mes);  
        this.checkLastMessage(this.dialogs.find(x => x.Id === dialog.Id), mes.OwnerId);
        this.startScroll();   
      }
      else if(dialog && this.selectedId !== dialog.Id && this.ownerId !== mes.OwnerId){ 
        this.checkLastMessage(this.dialogs.find(x => x.Id === dialog.Id), mes.OwnerId);
      }
      else if(!dialog){
        this.chatService.getDialogByOwner(mes.DialogId, this.ownerId).then(res => {          
          this.dialogs.push(res);
       });
      }
    }      
  }

  //check the last message: our or not
  checkLastMessage(dialog: DialogModel, mesOwner: number):DialogModel{
    if(this.ownerId !== mesOwner){
      dialog.IsReadedLastMessage = false;
      return dialog;
    }
    else{
      dialog.IsReadedLastMessage = true;
      return dialog;
    }
  }

  //check the messages we have sent were readed
  messagesWereReaded(){
    this.messages.filter(x => !x.IsReaded).forEach(mes => {
      if(mes.OwnerId === this.ownerId){
        mes.IsReaded = true;        
      }
    });
  }

  getDialog(){
    this.messages = undefined;
    return this.chatService.getDialog(this.selectedId).then(res => {
      this.dialog = res;
      this.messages = this.dialog.Messages;      
    });    
  }

  //events from keyboard
  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey ){      
        this.onWrite();  
      } 
      else{ 
        this.changeTextareaSize();
        this.readNotReadedMessages(this.dialog);
      }
    }, 0);
  }

  //select one from dialogs massif
  onSelect(dialogId: number) {    
    this.selectedId = dialogId;
    this.getDialog().then(() => this.startScroll());   
  }

  //check message we want to send 
  onWrite(){ 
    this.readNotReadedMessages(this.dialog);
    if(this.writtenMessage !== undefined){
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), "\n");
      str = str.replace((/\s{2,}/ig), " ");      
      if(str !== " " && str !== "\n" && str != ""){
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
  
  //send message
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
    this.chatEventsService.messageCreateFromChatToMiniChat(message); 
    this.startScroll();             
    this.chatService.addMessage(message);
  }


  //read messages were readed
  readNotReadedMessages(dialog: DialogModel){   
    if(dialog && dialog.Messages){
      let isChanged = false;
      dialog.Messages.filter(x => !x.IsReaded).forEach(mes => {
        if(mes.OwnerId !== this.ownerId){
          mes.IsReaded = true;
          isChanged = true;
        }
      });
      if(isChanged){
        dialog.IsReadedLastMessage = true;  
        this.dialogs.find(x => x.Id === dialog.Id).IsReadedLastMessage = true; 
        this.chatEventsService.messageReadFromChatToMiniChat(dialog.Id);           
        this.chatService.updateMessages(dialog.Id, this.ownerId);
      }
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
        this.cdr.detectChanges();
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
