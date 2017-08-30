import { Component, OnInit, Input } from '@angular/core';
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

  ownerId: string;  
  messages: MessageModel[];
  me: number;
  myParticipant: string;
  writtenMessage: string;  
  inputHeight: number = 43;  


  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    if(this.dialog.Id !== null){
      this.messages = this.dialog.Messages;
      this.me = this.dialog.ParticipantOneId;
      this.myParticipant = this.dialog.ParticipantName;
    }
    // this.ownerId = this.tokenHelper.getClaimByName('accountid');
    // this.chatService.getDialog(this.dialogId).then(res => {
    //   if(res !== undefined){
    //     this.dialog = res;
    //     if(this.dialog !== null && this.dialog.Messages.length !== 0){
          
    //     }
    //     else{
    //       this.messages = null;
    //     }
    //   }
    // });
  }

  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey){   
        if(this.dialog.Id === null){
          this.chatService.addDialog(this.dialog).then(res => {
            this.dialog = res;            
          });
        }   
        //this.onWrite();  
      } 
      else{  
          // this.textarea.nativeElement.style.height = 0 + 'px';
          // var height = this.textarea.nativeElement.scrollHeight;      
          // this.textarea.nativeElement.style.height = height + 2 + 'px';
      }
    }, 0);
  }

}
