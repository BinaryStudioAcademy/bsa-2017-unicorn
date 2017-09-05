import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ChatEventsService } from "../services/events/chat-events.service";
import { DialogModel } from "../models/chat/dialog.model";

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.sass']
})
export class ShellComponent implements OnInit {
  openChat: Subscription;
  closeChat: Subscription;
  dialog: DialogModel;
  isChatOpen: boolean = false;

  constructor(private chatEventsService: ChatEventsService) {
  }

  ngOnInit() {
    this.openChat = this.chatEventsService.openChatEvent$
      .subscribe(dial => {        
        this.dialog = dial;
        this.isChatOpen = true;
        this.chatEventsService.initChat(this.dialog);     
      });

    this.closeChat = this.chatEventsService.closeChatEvent$
      .subscribe(() => this.isChatOpen = false);
  }

  ngOnDestroy() {
    this.openChat.unsubscribe();
    this.closeChat.unsubscribe();
  }



}
