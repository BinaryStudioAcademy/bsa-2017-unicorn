import { Injectable, ChangeDetectorRef } from '@angular/core';
import { DialogModel } from "../../models/chat/dialog.model";
import { MessageModel } from "../../models/chat/message.model";
import { NgClass } from '@angular/common';
import { ChatService } from "./chat.service";
import { TokenHelperService } from "../helper/tokenhelper.service";

@Injectable()
export class ChatLogicService {

    private messagesElement: any;
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


    constructor(private chatService: ChatService,
        private tokenHelper: TokenHelperService,
        private cdr: ChangeDetectorRef) { }


    initialize(parametres: any): any{
        parametres.ownerId = +this.tokenHelper.getClaimByName('accountid');    
        this.chatService.getDialogs(parametres.ownerId).then(res => {
          if(res !== undefined && res !== null){
            parametres.dialogs = res;
            if(parametres.dialogs !== null && parametres.dialogs.length !== 0){
                parametres.containerHeight = 300;
                parametres.selectedId = parametres.dialogs[0].Id;
              let result = this.getDialog({
                  dialog: parametres.dialog,
                  selectedId: parametres.selectedId,
                  messages: parametres.messages
              }); 
              parametres.dialog = result.dialog;
              parametres.selectedId = result.selectedId;
              parametres.messages = result.messages;
            }
            else{
                parametres.messages = [];
                parametres.containerHeight = 150;
            }
          }
          return parametres;      
        });    
      }

      getDialog(parametres: any):any{
        this.chatService.getDialog(parametres.selectedId).then(res => {
            parametres.dialog = res;
            parametres.messages = parametres.dialog.Messages; 
            return parametres;      
        });           
      }

      onChange(parametres: any){  
        setTimeout(() => {
          if(parametres.event.key === "Enter" && !parametres.event.shiftKey){      
            this.onWrite();  
          } 
          else{ 
            this.changeTextareaSize();
          }
        }, 0);
      }

      onSelect(parametres: any) {    
        parametres.selectedId = parametres.dialogId;
        let result = this.getDialog({
            dialog: parametres.dialog,
            selectedId: parametres.selectedId,
            messages: parametres.messages
        }); 
        this.startScroll();   
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
          MessageId: null,
          DialogId: this.selectedId,
          IsReaded: false, 
          OwnerId: this.ownerId,
          Message: this.writtenMessage,
          Files: null,
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