import { Component, OnInit, ViewChild, ChangeDetectorRef, ElementRef, HostListener } from '@angular/core';
import { DialogModel } from "../models/chat/dialog.model";
import { MessageModel } from "../models/chat/message.model";
import { ChatFile } from "../models/chat/chat-file";
import { NgClass } from '@angular/common';
import { ChatService } from "../services/chat/chat.service";
import { TokenHelperService } from "../services/helper/tokenhelper.service";
import { NotificationService } from "../services/notifications/notification.service";
import { Subscription } from "rxjs/Subscription";
import { ChatEventsService } from "../services/events/chat-events.service";
import { ProfileShortInfo } from "../models/profile-short-info.model";
import { AccountService } from "../services/account.service";
import { ModalTemplate, SuiActiveModal, TemplateModalConfig, ModalSize, SuiModalService } from "ng2-semantic-ui";
export interface IDelete {
  id: number;
}
@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.sass']
})
export class ChatComponent implements OnInit {
  @ViewChild('messagesBlock')
  private messagesElement: any;
  @ViewChild('textArea')
  @ViewChild('fileInput') inputEl: ElementRef;
  private textarea: any;
  @ViewChild('modal')
  public deleteTemplate:ModalTemplate<IDelete, void, void>
  currModal: SuiActiveModal<IDelete, {}, void>;
  ownerId: number;
  dialogs: DialogModel[];
  dialog: DialogModel;
  messages: MessageModel[];
  selectedId: number;
  writtenMessage: string;
  inputHeight: number = 43;
  containerHeight = 500;
  noMessages: boolean = true;
  needScroll: boolean = false;
  files: ChatFile[];

  dialogCreate: Subscription;
  messageCreate: Subscription;
  messageRead: Subscription;

  searchString: string;
  searchResults: ProfileShortInfo[];
  messageToDelete;
  dialogToDelete;

  sideBarEnabled: boolean = true;
  windowWidth: number = window.innerWidth;
  hideButtonClicked = false;


  constructor(private chatService: ChatService,
    private tokenHelper: TokenHelperService,
    private cdr: ChangeDetectorRef,
    private notificationService: NotificationService,
    private chatEventsService: ChatEventsService,
    private accountService: AccountService,
    private modalService: SuiModalService) { }

  ngAfterViewInit() {
      this.windowWidth = window.innerWidth;
      if(this.windowWidth <= 800){
        this.sideBarEnabled = false;
      }
  }

  @HostListener('window:resize', ['$event'])
  resize(event) {
      this.windowWidth = window.innerWidth;
      if(window.innerWidth > 800){
        this.sideBarEnabled = true;
      }
      else if(window.innerWidth <= 800){
        this.sideBarEnabled = false;
      }
  }

  hideResponsiveSideBar(){
    this.hideButtonClicked = true;    
    if(this.sideBarEnabled){
    this.sideBarEnabled = false;
    }
    else{
      this.sideBarEnabled = true;
    }    
  }

  clickOutsideSideBar(){    
    if(this.sideBarEnabled && this.windowWidth <= 800 && !this.hideButtonClicked){
      this.sideBarEnabled = false;
    }  
    this.hideButtonClicked = false;
  }

  ngOnInit() {
    this.getDialogs().then(() => this.startScroll());
    this.notificationService.listen<any>("RefreshMessages", res => {
      this.getMessage(res);
    });
    this.notificationService.listen<any>("ReadNotReadedMessages", () => {
     this.messagesWereReaded();
    });

    this.notificationService.listen<any>("DeleteMessage", () => {
      this.getDialog();
     });
     
    this.dialogCreate = this.chatEventsService.createDialogFromMiniChatToChatEvent$.subscribe(dialog => {
      this.dialogs.push(this.checkLastMessage(dialog, dialog.Messages[dialog.Messages.length - 1].OwnerId));
    })
    this.messageCreate = this.chatEventsService.createMessageFromMiniChatToChatEvent$.subscribe(mes => {
  
      if (this.dialogs.find(x => x.Id === mes.DialogId)) {
        if (this.dialog.Id === mes.DialogId) {
          var dial = this.dialogs.find(x => x.Id === mes.DialogId);
          
        if(this.isHided(dial))
          {  
             dial.Participant1_Hided = false;
             dial.Participant2_Hided = false;
             this.getDialog();
          } else
          this.messages.push(mes);
          this.startScroll();
        }
      }
    });

    this.messageRead = this.chatEventsService.readMessageFromMiniChatToChatEvent$.subscribe(dialogId => {
      if (this.dialogs.find(x => x.Id === dialogId)) {
        if (this.dialog.Id === dialogId) {
          this.readNotReadedMessages(this.dialog);
          if (!this.dialog.IsReadedLastMessage) {
            this.dialog.IsReadedLastMessage = true;
          }
        }
        else {
          this.dialogs.find(x => x.Id === dialogId).IsReadedLastMessage = true;
        }
      }
    });
    this.searchResults = [];
  }

  ngAfterViewChecked() {
    if (this.needScroll) {
      this.scrollMessages();
    }
  }
  deleteMessage()
  {
   this.chatService.deleteMessage(this.messageToDelete.MessageId).then(res=>{this.getDialog();
    this.chatEventsService.messageDeleteFromChatToMiniChat(this.messageToDelete);});
   
   this.currModal.deny(undefined);
   this.startScroll();
   
  }
  deleteDialog()
  {
    this.chatService.deleteDialog(this.dialogToDelete.Id, +this.tokenHelper.getClaimByName('accountid')).then(res=>this.getDialogs());
    this.currModal.deny(undefined);
    
  }
  deleteDialogModal(dialog: DialogModel)
  {
    this.dialogToDelete = dialog;
    this.messageToDelete = undefined;
    const config = new TemplateModalConfig<IDelete, void, void>(this.deleteTemplate);
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }

  deleteMessageModal(message: MessageModel)
  {
    this.dialogToDelete = undefined;
    this.messageToDelete = message;
    const config = new TemplateModalConfig<IDelete, void, void>(this.deleteTemplate);
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }
  getDialogs() {
    this.ownerId = +this.tokenHelper.getClaimByName('accountid');
    return this.chatService.getDialogs(this.ownerId).then(res => {
      if (res !== undefined) {
        this.dialogs = res;
        if (this.dialogs !== null && this.dialogs.length !== 0) {
          var i = 0;
          while(i<this.dialogs.length && this.isHided(this.dialogs[i]))
            i++;
          if(i<this.dialogs.length)
         { this.selectedId = this.dialogs[i].Id;
          return this.getDialog();
         } else this.messages=[]; 
        }
        else {
          this.messages = [];
          this.dialogs = [];
        }
      }
    });
  }
  
  isHided(_dialog: DialogModel):boolean
  {
    if(_dialog){
      if(_dialog.Participant1_Hided && _dialog.ParticipantOneId==this.ownerId) {
        return true;
      } 
      else if(_dialog.Participant2_Hided && _dialog.ParticipantTwoId==this.ownerId){
        return true;
      } 
      else{
      return false;
      }
    }
  }

  //get message, if anybody sent one to us
  getMessage(mes: MessageModel) {
    if (!this.dialogs) {
      this.getDialogs().then(() => {
        this.startScroll();
      });
    }
    else {
      let dialog = this.dialogs.find(x => x.Id === mes.DialogId);
    
      if (dialog && this.selectedId === dialog.Id) {
        if(this.isHided(dialog))
        { 
           dialog.Participant1_Hided = false;
           dialog.Participant2_Hided = false;
           this.getDialog();
        } else
        this.messages.push(mes);
        this.checkLastMessage(this.dialogs.find(x => x.Id === dialog.Id), mes.OwnerId);
        this.startScroll();
      }
      else if (dialog && this.selectedId !== dialog.Id && this.ownerId !== mes.OwnerId) {
        if(this.isHided(dialog))
        {  
           dialog.Participant1_Hided = false;
           dialog.Participant2_Hided = false;
        }
        this.checkLastMessage(this.dialogs.find(x => x.Id === dialog.Id), mes.OwnerId);
      }
      else if (!dialog) {
        if(this.isHided(dialog))
        {  
           dialog.Participant1_Hided = false;
           dialog.Participant2_Hided = false;
        }
        this.chatService.getDialogByOwner(mes.DialogId, this.ownerId).then(res => {
          this.dialogs.push(res);
        });
      }
    }
  }

  //check the last message: our or not
  checkLastMessage(dialog: DialogModel, mesOwner: number): DialogModel {
    if (this.ownerId !== mesOwner) {
      dialog.IsReadedLastMessage = false;
      return dialog;
    }
    else {
      dialog.IsReadedLastMessage = true;
      return dialog;
    }
  }

  //check the messages we have sent were readed
  messagesWereReaded() {
    this.messages.filter(x => !x.IsReaded).forEach(mes => {
      if (mes.OwnerId === this.ownerId) {
        mes.IsReaded = true;
      }
    });
  }

  getDialog() {
    //this.messages = undefined;
    return this.chatService.getDialog(this.selectedId).then(res => {
      this.dialog = res;
      this.messages = this.dialog.Messages;
      this.startScroll();
    });
  }

  //events from keyboard
  onChange(event) {
    setTimeout(() => {
      if (event.key === "Enter" && !event.shiftKey) {
        this.onWrite();
      }
      else {
        this.changeTextareaSize();
        this.readNotReadedMessages(this.dialog);
      }
    }, 0);
  }

  //select one from dialogs massif
  onSelect(dialogId: number) {
    this.selectedId = dialogId;

    if (this.selectedId !== null)
      this.getDialog().then(() => this.startScroll());
    else
      this.messages = [];

    this.searchString = '';
    this.searchResults = [];
  }

  //check message we want to send 
  onWrite() {
    this.readNotReadedMessages(this.dialog);
    if (this.writtenMessage !== undefined) {
      let str = this.writtenMessage;
      str = str.replace((/\n{2,}/ig), "\n");
      str = str.replace((/\s{2,}/ig), " ");
      if (str !== " " && str !== "\n" && str != "") {
        this.writtenMessage = this.writtenMessage.trim();
        this.addMessage();
        this.writtenMessage = undefined;
      }
      else {
        this.writtenMessage = undefined;
      }
    }    
    this.normalTeaxareaSize();
  }

  //send message
  addMessage() {
    if (this.selectedId === null) {
      let message = this.writtenMessage;
      this.chatService.addDialog(this.dialog)
        .then(resp => {
          this.dialog.Id = resp.Id;
          this.selectedId = resp.Id;
          this.writtenMessage = message;
          this.addMessage();
          this.writtenMessage = undefined;
          return;
        });
    } else {
      let message = {
        MessageId: null,
        DialogId: this.selectedId,
        IsReaded: false,
        OwnerId: this.ownerId,
        Message: this.writtenMessage,
        Files: this.files,
        Date: new Date(),
        isLoaded: true
      };
      this.messages.push(message);
      this.chatEventsService.messageCreateFromChatToMiniChat(message);
      this.startScroll();
      this.chatService.addMessage(message).then(x=>{
            this.chatService.getDialog(this.selectedId).then(res => {
            this.dialog = res;
            this.messages = this.dialog.Messages;                
          });   
        });
      this.files = null;

      if(this.writtenMessage !== undefined) {
        this.writtenMessage = undefined;
      }
    }
  }


  //read messages were readed
  readNotReadedMessages(dialog: DialogModel) {
    if (dialog && dialog.Messages) {
      let isChanged = false;
      dialog.Messages.filter(x => !x.IsReaded).forEach(mes => {
        if (mes.OwnerId !== this.ownerId) {
          mes.IsReaded = true;
          isChanged = true;
        }
      });
      if (isChanged) {
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

  scrollMessages() {
    if (this.messagesElement && this.messagesElement.nativeElement.scrollTop !== this.messagesElement.nativeElement.scrollHeight) {
      this.messagesElement.nativeElement.scrollTop = this.messagesElement.nativeElement.scrollHeight;
      this.needScroll = false;
      this.noMessages = false;
      this.cdr.detectChanges();
    }
  }
  normalTeaxareaSize() {
    if (this.textarea !== undefined) {
      this.textarea.nativeElement.style.height = 43 + 'px';
    }
  }
  changeTextareaSize() {
    if (this.textarea !== undefined) {
      this.textarea.nativeElement.style.height = 0 + 'px';
      var height = this.textarea.nativeElement.scrollHeight;
      this.textarea.nativeElement.style.height = height + 2 + 'px';
    }
  }

  filterDialogsByPartitipantName(name: string): DialogModel[] {
    if (this.dialogs && this.dialogs !== null) {
      if (name && name !== '')
        return this.dialogs.filter(d => d.ParticipantName.toLowerCase().includes(name.toLowerCase()));
      else
        return this.dialogs;
    }
  }

  searchNewPartitipants() {
    this.accountService.searchByTemplate(this.searchString, 20)
      .then(resp => this.searchResults = resp.filter(x =>
        this.dialogs.find(d =>
          d.ParticipantOneId === x.AccountId || d.ParticipantTwoId === x.AccountId) === undefined ||
        this.isHided(
          this.dialogs.find(d =>
            d.ParticipantOneId === x.AccountId || d.ParticipantTwoId === x.AccountId)
        )));
  }

  createChat(partitipant: ProfileShortInfo) {
   var dial = this.dialogs.find(d =>
    d.ParticipantOneId === partitipant.AccountId || d.ParticipantTwoId === partitipant.AccountId);
   if(dial  === undefined)
{
    while (this.dialogs.find(x => !x.Id || x.Id === null) !== undefined) {
      this.dialogs.splice(this.dialogs.findIndex(x => !x.Id || x.Id === null), 1);
    }

    this.dialog = {
      Id: null,
      ParticipantOneId: this.ownerId,
      ParticipantTwoId: partitipant.AccountId,
      ParticipantName: partitipant.Name,
      ParticipantAvatar: partitipant.Avatar,
      ParticipantProfileId: null,
      ParticipantType: partitipant.Role,
      Messages: null,
      LastMessageTime: null,
      IsReadedLastMessage: true,
      Participant1_Hided: false,
      Participant2_Hided: false
    };

    this.selectedId = this.dialog.Id;
    this.dialogs.push(this.dialog);
    this.messages = [];
    this.noMessages = false;
    this.searchString = '';
    this.searchResults = [];
  } else
 { 
   this.dialog = dial;
   this.selectedId = dial.Id;
   if(this.dialog.ParticipantOneId==this.ownerId)
    this.dialog.Participant1_Hided = false; else
    this.dialog.Participant2_Hided = false;
   this.getDialog();
   this.chatService.deleteDialog(dial.Id,this.ownerId);
   this.searchString = '';
   this.searchResults = [];
 }
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
    this.dialogCreate.unsubscribe();
    this.messageCreate.unsubscribe();
    this.messageRead.unsubscribe();
  }
}
