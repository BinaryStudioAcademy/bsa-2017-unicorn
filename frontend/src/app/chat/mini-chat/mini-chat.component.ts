import { Component, OnInit, Input, ViewChild, ChangeDetectorRef } from '@angular/core';
import { MessageModel } from "../../models/chat/message.model";
import { ChatService } from "../../services/chat/chat.service";
import { TokenHelperService } from "../../services/helper/tokenhelper.service";
import { DialogModel } from "../../models/chat/dialog.model";
import { ChatEventsService } from "../../services/events/chat-events.service";
import { NotificationService } from "../../services/notifications/notification.service";

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


  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService,
    private chatEventsService: ChatEventsService,
    private cdr: ChangeDetectorRef,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.initializeDialog();
    this.notificationService.listen<any>("RefreshMessages", res => {
      this.getMessage(res);
    }); 
    this.notificationService.listen<any>("ReadNotReadedMessages", () => {
      this.messagesWereReaded();
    }); 
  }

  ngAfterViewChecked() {
    if (this.needScroll) {
      this.scrollMessages();      
    }    
  }

  initializeDialog(){
    this.ownerId = +this.tokenHelper.getClaimByName('accountid');    
    this.dialog.Messages === null ? this.messages = [] : this.messages = this.dialog.Messages;
    this.startScroll();    
  }

  getMessage(mes: MessageModel){
    if(this.dialog.Id === null){
      let participantId = this.ownerId === this.dialog.ParticipantOneId ? this.dialog.ParticipantTwoId : this.dialog.ParticipantOneId;
      this.chatService.findDialog(this.ownerId, participantId).then(res => {
        if(res){
          this.dialog = res;
          this.messages = this.dialog.Messages;
          this.startScroll(); 
        }
      })
    }
    else{
      this.messages.push(mes);     
      this.startScroll();   
    }      
  }

  messagesWereReaded(){
    this.messages.filter(x => !x.IsReaded).forEach(mes => {
      if(mes.OwnerId === this.ownerId){
        mes.IsReaded = true;        
      }
    });
  }

  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey){ 
        this.onWrite();
      }      
    }, 0);
  }

  onWrite(){  
    this.readNotReadedMessages();
    this.noMessages = true; 
    if(this.writtenMessage !== undefined){      
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), "\n");
      str = str.replace((/\s{2,}/ig), " ");      
      if(str !== " " && str !== "\n" && str != ""){
        this.writtenMessage = this.writtenMessage.trim();
        if(this.dialog.Id === null){          
          this.chatService.addDialog(this.dialog).then(res => {
            this.dialog.Id = res.Id;            
            this.addMessage();      
            this.writtenMessage = undefined;                   
          });
        }
        else{
          this.addMessage();
        }  
      }
      else{
        this.writtenMessage = undefined;
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
    this.writtenMessage = undefined;
    this.messages.push(message);     
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

  closeChat(){
    this.chatEventsService.closechat();
  }

}
