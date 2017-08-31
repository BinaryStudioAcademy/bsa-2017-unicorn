import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { MessageModel } from "../../models/chat/message.model";
import { ChatService } from "../../services/chat/chat.service";
import { TokenHelperService } from "../../services/helper/tokenhelper.service";
import { DialogModel } from "../../models/chat/dialog.model";

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
  me: number;
  myParticipant: string;
  writtenMessage: string;  
  inputHeight: number = 33;
  noMessages: boolean = false;  


  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    this.ownerId = +this.tokenHelper.getClaimByName('accountid');    
    this.dialog.Messages === null ? this.messages = [] : this.messages = this.dialog.Messages;
    this.me = this.dialog.ParticipantOneId;
    this.myParticipant = this.dialog.ParticipantName;  
    this.scrollMessages();
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

  onWrite(){  
    if(this.writtenMessage !== undefined){      
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), "\n");
      str = str.replace((/\s{2,}/ig), " ");      
      if(str !== " " && str !== "\n"){
        this.writtenMessage = this.writtenMessage.trim();
        if(this.dialog.Id === null){
          this.noMessages = true;
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
    this.scrollMessages();      
    this.chatService.addMessage(message).then(res =>  {     
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


  scrollMessages(){ 
    this.noMessages = true;  
    setTimeout(() => {
      if(this.messagesElement !== undefined){
        this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
      }
      this.noMessages = false;
    }, 0);
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

}
