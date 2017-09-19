import { Component, OnInit, Input, ViewChild, ChangeDetectorRef, ElementRef } from '@angular/core';
import { MessageModel } from "../../models/chat/message.model";
import { ChatFile } from "../../models/chat/chat-file";
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
  @ViewChild('fileInput') inputEl: ElementRef;

  ownerId: number;  
  messages: MessageModel[];  
  writtenMessage: string;  
  inputHeight: number = 33;
  noMessages: boolean = false;
  needScroll: boolean = false;  
  openedDialogs: DialogModel[] = [];
  selectedId: number;
  collapsedChat:boolean = false;
  files: ChatFile[];

  initChat: Subscription;
  messageCreate: Subscription;
  messageRead: Subscription;
  messageDelete: Subscription;

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
    this.notificationService.connect(this.ownerId); 
    this.notificationService.listen<any>("RefreshMessages", res => {
      this.getMessage(res);
    }); 
    this.notificationService.listen<any>("ReadNotReadedMessages", dialId => {
      this.messagesWereReaded(dialId);
    }); 

    this.messageCreate = this.chatEventsService.createMessageFromChatToMiniChatEvent$.subscribe(mes => {
      if(this.openedDialogs.find(x => x.Id === mes.DialogId)){
        if(this.dialog.Id === mes.DialogId){
          this.messages.push(mes);     
          this.startScroll();   
        }
        else{
           this.openedDialogs.find(x => x.Id === mes.DialogId).Messages.push(mes);
        }
      }
    });
    this.messageDelete = this.chatEventsService.deleteMessageFromChatToMiniChatEvent$.subscribe(mes=>{
      {
        this.chatService.getDialog(mes.DialogId).then(res => this.openedDialogs
          .find(x => x.Id === mes.DialogId).Messages = res.Messages);
        if(this.dialog.Id === mes.DialogId)
        {
         this.chatService.getDialog(mes.DialogId).then(res => this.messages = res.Messages)
          this.startScroll();   
        }
      }
    });
    this.messageRead = this.chatEventsService.readMessageFromChatToMiniChatEvent$.subscribe(dialogId => { 
      if(this.dialog.Id === dialogId){
        this.readNotReadedMessages(this.dialog);
        if(!this.dialog.IsReadedLastMessage){
          this.dialog.IsReadedLastMessage= true;
        }
      }
      else{
        this.readNotReadedMessages(this.openedDialogs.find(x => x.Id === dialogId));
        if(!this.openedDialogs.find(x => x.Id === dialogId).IsReadedLastMessage){
          this.openedDialogs.find(x => x.Id === dialogId).IsReadedLastMessage= true;
        }
      }
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

  //get message, if anybody sent one to us
  getMessage(mes: MessageModel){   
    if(this.openedDialogs.length !== 0){
      let dialog = this.openedDialogs.find(x => x.Id === mes.DialogId);
      if(dialog && this.selectedId !== dialog.Id){
        this.openedDialogs.find(x => x.Id === mes.DialogId).Messages.push(mes);
        if(mes.OwnerId !== this.ownerId){
          this.openedDialogs.find(x => x.Id === mes.DialogId).IsReadedLastMessage = false;
        }
        return;
      }
      else if(dialog && this.selectedId === dialog.Id){        
        this.dialog.Messages.push(mes);  
        if(mes.OwnerId !== this.ownerId){
          this.dialog = this.checkLastMessage(this.dialog, this.dialog.Messages[this.dialog.Messages.length - 1].OwnerId);
        }          
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


  findDialog(participantOneId: number, participantTwoId: number){    
    let participantId = this.ownerId === participantOneId ? participantTwoId : participantOneId;
    return this.chatService.findDialog(this.ownerId, participantId);
  }

  //shift and push in opened dialogs massif (depends on massif length)
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

  //check the messages we have sent were readed
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

  //events from keyboard
  onChange(event){  
    setTimeout(() => {
      if(event.key === "Enter" && !event.shiftKey){         
        this.onWrite();
      }
      else{        
        this.readNotReadedMessages(this.dialog);
      }  
    }, 0);
  }

  //select one from dialogs massif
  onSelect(dialogId: number) { 
    this.collapsedChat = false;   
    this.selectedId = dialogId;
    this.dialog = this.openedDialogs.find(x => x.Id === dialogId);
    this.messages = this.dialog.Messages;
    this.startScroll();  
  }

  //check message we want to send 
  onWrite(){
    this.readNotReadedMessages(this.dialog);   
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
            this.dialog.LastMessageTime = res.LastMessageTime;
            this.selectedId = this.dialog.Id;
            this.addMessage(); 
            this.workWithDialogs(this.dialog);  
            this.writtenMessage = undefined;   
            this.chatEventsService.dialogCreateFromMiniChatToChat(this.dialog);
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


  //send message
  addMessage(){     
    let message = {
      MessageId: null,
      DialogId: this.dialog.Id,
      IsReaded: false, 
      OwnerId: this.ownerId,
      Message: this.writtenMessage,
      Files: this.files,
      Date: new Date(),
      isLoaded: true
    };   
    if(!this.dialog.Messages){
      this.dialog.Messages = [];
    }
    this.dialog.Messages.push(message);    
    this.messages = this.dialog.Messages; 
    this.startScroll();             
    this.chatService.addMessage(message).then(res=>{
      this.chatService.getDialog(this.dialog.Id)
      .then(res=> this.chatEventsService.messageCreateFromMiniChatToChat(res.Messages[res.Messages.length-1]))
      
    })
    this.files = null;    

    if(this.writtenMessage !== undefined) {
      this.writtenMessage = undefined;
    }
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
        this.openedDialogs.find(x => x.Id === dialog.Id).IsReadedLastMessage = true;
        this.chatEventsService.messageReadFromMiniChatToChat(dialog.Id);        
        this.chatService.updateMessages(dialog.Id, this.ownerId);
      }  
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
    if(dialId === this.dialog.Id) {
      this.openedDialogs = this.openedDialogs.filter(x => x.Id !== this.dialog.Id);
      this.collapsedChat = true;
    }
    else{
      this.openedDialogs = this.openedDialogs.filter(x => x.Id !== this.dialog.Id);
    }    
  }

  collapseChat(){
    this.collapsedChat = true;
    this.selectedId = undefined;
  }

  isImage(filename: string){
    const imgExtensions = ["jpg", "jpeg", "bmp", "png", "ico"];
    let extension = filename.split(".").pop();
    
    return imgExtensions.includes(extension);
  }

  uploadFile() {
    let inputEl: HTMLInputElement = this.inputEl.nativeElement;        
    let fileCount: number = inputEl.files.length;
    let formData = new FormData();
    if (fileCount > 0) {
      for (let i = 0; i < fileCount; i++) {
        formData.append('file[]', inputEl.files.item(i));
      }
      
      this.chatService.uploadFiles(formData).then(x => {
        this.files = x as ChatFile[];
        this.addMessage();
      }).catch(err => console.log(err));
            
      inputEl.value = null;
    }
  }

  ngOnDestroy() {
    this.messageDelete.unsubscribe();
    this.messageCreate.unsubscribe();
    this.messageRead.unsubscribe();
  }

}
