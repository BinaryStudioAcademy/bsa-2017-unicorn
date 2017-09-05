import { Component, OnInit, Input, ViewChild, ChangeDetectorRef } from '@angular/core';
import { MessageModel } from "../../models/chat/message.model";
import { ChatService } from "../../services/chat/chat.service";
import { TokenHelperService } from "../../services/helper/tokenhelper.service";
import { DialogModel } from "../../models/chat/dialog.model";
import { ChatEventsService } from "../../services/events/chat-events.service";
import { NotificationService } from "../../services/notifications/notification.service";
import { Subscription } from "rxjs/Subscription";

@Component({
  selector: 'app-mini-chat',
  templateUrl: './mini-chat.component.html',
  styleUrls: ['./mini-chat.component.sass']
})
export class MiniChatComponent implements OnInit {
  @Input()
    dialog: DialogModel;

  @ViewChild('messagesBlock')
    private messagesElement: any;
  
  @ViewChild('textArea')
    private textarea: any;

  ownerId: number;  
  messages: MessageModel[];  
  writtenMessage: string;  
  inputHeight: number = 33;
  noMessages: boolean = false;
  needScroll: boolean = false;  
  openedDialogs: DialogModel[] = [];
  selectedId: number;
  collapsedChat:boolean = false;

  initChat: Subscription;


  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService,
    private chatEventsService: ChatEventsService,
    private cdr: ChangeDetectorRef,
    private notificationService: NotificationService) { }

  ngOnInit() {   
    this.initChat = this.chatEventsService.initChatEvent$
    .subscribe(dial => {      
      this.dialog = dial;      
      this.initializeDialog();     
    });
    
    this.initializeDialog();     
    this.notificationService.listen<any>("RefreshMessages", res => {
      this.getMessage(res);
    }); 
    this.notificationService.listen<any>("ReadNotReadedMessages", dialId => {
      this.messagesWereReaded(dialId);
    }); 
  }

  ngAfterViewChecked() {
    if (this.needScroll) {
      this.scrollMessages();      
    }    
  }

  initializeDialog(){    
    this.collapsedChat = false;
    this.selectedId = this.dialog.Id;
    this.messages = this.dialog.Messages === null ? [] : this.dialog.Messages;
    this.ownerId = +this.tokenHelper.getClaimByName('accountid'); 
    this.workWithDialogs(this.dialog);
    this.startScroll();    
  }

  getMessage(mes: MessageModel){   
    if(this.openedDialogs.length !== 0){
      let dialog = this.openedDialogs.find(x => x.Id === mes.DialogId);
      if(dialog && this.selectedId !== dialog.Id){
        this.openedDialogs.find(x => x.Id === mes.DialogId).Messages.push(mes);
        this.openedDialogs.find(x => x.Id === mes.DialogId).IsReadedLastMessage = false;
        return;
      }
      else if(dialog && this.selectedId === dialog.Id){
        this.dialog.IsReadedLastMessage = false;
        this.dialog.Messages.push(mes);            
        this.startScroll();   
        return;
      }
      else if(!dialog){       
        this.chatService.getDialogByOwner(mes.DialogId, this.ownerId).then(res => {          
          this.workWithDialogs(res);
       });
      }
    }
    else if(this.dialog.Id === null){
      this.findDialog(this.dialog.ParticipantOneId, this.dialog.ParticipantTwoId).then(res => {
        if(res){
          this.dialog = res;
          this.messages = this.dialog.Messages;
          this.workWithDialogs(this.dialog);
          this.startScroll(); 
        }
      });;
    }
    else{
      this.messages.push(mes);     
      this.startScroll();   
    }      
  }

  findDialog(participantOneId: number, participantTwoId: number){    
    let participantId = this.ownerId === participantOneId ? participantTwoId : participantOneId;
    return this.chatService.findDialog(this.ownerId, participantId);
  }

  workWithDialogs(dialog: DialogModel){       
    if(dialog.Id && !this.openedDialogs.find(x => x.Id === dialog.Id)){
      if(this.openedDialogs.length === 5){
        this.openedDialogs.shift();
        this.openedDialogs.push(dialog);
      }
      else{
        this.openedDialogs.push(dialog);
      }
    }
  }

  messagesWereReaded(dialId: number){
    if(this.openedDialogs.length !== 0){
      let dialog = this.openedDialogs.find(x => x.Id === dialId);
      if(dialog){
        dialog.Messages.filter(x => !x.IsReaded).forEach(mes => {
          if(mes.OwnerId === this.ownerId){
            mes.IsReaded = true;        
          }
        });
      }
    }    
  }

  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey){         
        this.onWrite();
      }      
    }, 0);
  }

  onSelect(dialogId: number) { 
    this.collapsedChat = false;   
    this.selectedId = dialogId;
    this.dialog = this.openedDialogs.find(x => x.Id === dialogId);
    this.messages = this.dialog.Messages;
    this.startScroll();  
  }

  onWrite(){        
    this.readNotReadedMessages();    
    if(this.writtenMessage !== undefined){      
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), "\n");
      str = str.replace((/\s{2,}/ig), " ");      
      if(str !== " " && str !== "\n" && str != ""){
        this.noMessages = true;  
        this.writtenMessage = this.writtenMessage.trim();
        if(!this.dialog || this.dialog.Id === null){          
          this.chatService.addDialog(this.dialog).then(res => {            
            this.dialog.Id = res.Id; 
            this.dialog.ParticipantAvatar = res.ParticipantAvatar;   
            this.addMessage(); 
            this.workWithDialogs(this.dialog);  
            this.writtenMessage = undefined;                   
          });
        }
        else{
          this.addMessage();
          this.writtenMessage = undefined;
        }  
      }
      else{
        this.writtenMessage = undefined;
        this.startScroll();
      }
    }
    this.normalTeaxareaSize();  
  }   


  addMessage(){     
    let message = {
      DialogId: this.dialog.Id,
      IsReaded: false, 
      OwnerId: this.ownerId,
      Message: this.writtenMessage, 
      Date: new Date(),
      isLoaded: true
    };   
    if(!this.dialog.Messages){
      this.dialog.Messages = [];
    }     
    this.dialog.Messages.push(message);    
    this.messages = this.dialog.Messages;    
    this.startScroll();             
    this.chatService.addMessage(message);
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
      this.dialog.IsReadedLastMessage = true;
      this.chatService.updateMessages(this.dialog.Id, this.ownerId);
    }
  }


  startScroll() {
    this.needScroll = true;    
  }

  scrollMessages(){
      if (this.messagesElement && this.messagesElement.nativeElement.scrollTop !== this.messagesElement.nativeElement.scrollHeight) {
        this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight; 
        this.needScroll = false;  
        this.noMessages = false;
        this.cdr.detectChanges();       
      }
  }
  normalTeaxareaSize(){
    if(this.textarea !== undefined){ 
      this.textarea.nativeElement.style.height = 33 + 'px';     
    }
  }
  changeTextareaSize(){
    if(this.textarea !== undefined){ 
      this.textarea.nativeElement.style.height = 0 + 'px';
      var height = this.textarea.nativeElement.scrollHeight;      
      this.textarea.nativeElement.style.height = height + 2 + 'px';
    }
  }

  closeChat(dialId: number){
    if(dialId === this.selectedId) {
      this.openedDialogs = this.openedDialogs.filter(x => x.Id !== this.dialog.Id);
      this.collapsedChat = true;
    }
    else{
      this.openedDialogs = this.openedDialogs.filter(x => x.Id !== this.dialog.Id);
    }
    // this.chatEventsService.closechat();
  }

  collapseChat(){
    this.collapsedChat = true;
    this.selectedId = undefined;
  }

}
