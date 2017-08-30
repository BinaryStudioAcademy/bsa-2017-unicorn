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

  ownerId: string;  
  messages: MessageModel[];
  me: number;
  myParticipant: string;
  writtenMessage: string;  
  inputHeight: number = 33;  


  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    console.log(this.dialog);  
    this.dialog.Messages === null ? this.messages = [] : this.messages = this.dialog.Messages;
    this.me = this.dialog.ParticipantOneId;
    this.myParticipant = this.dialog.ParticipantName;    
    console.log(this.messages);    
  }

  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey){   
        if(this.dialog.Id === null){
          this.chatService.addDialog(this.dialog).then(res => {
            this.dialog.Id = res;            
          });
        }   
        this.onWrite();  
      } 
      else{  
          this.textarea.nativeElement.style.height = 0 + 'px';
          var height = this.textarea.nativeElement.scrollHeight;      
          this.textarea.nativeElement.style.height = height + 2 + 'px';
      }
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
    this.textarea.nativeElement.style.height = 33 + 'px';     
  }   


  addMessage(){
    let message = {
      DialogId: this.dialog.Id,
      IsReaded: false, 
      OwnerId: this.me,
      Message: this.writtenMessage, 
      Date: new Date(),
      isLoaded: true
    };
    this.writtenMessage = undefined;
    this.messages.push(message); 
    setTimeout(() => {
      this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
    }, 0);   
    this.chatService.addMessage(message).then(res =>  {
      console.log(res);
      this.messages.find(x => x.isLoaded).isLoaded = false;
    });
  }

}
