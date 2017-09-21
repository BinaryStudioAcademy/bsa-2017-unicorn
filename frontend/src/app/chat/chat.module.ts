import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ChatComponent } from "./chat.component";
import { FormsModule } from '@angular/forms'
import { MomentModule } from "angular2-moment";
import { ChatService } from "../services/chat/chat.service";
import { MiniChatComponent } from './mini-chat/mini-chat.component';
import { SuiModule } from 'ng2-semantic-ui';
import { ClickOutsideModule } from 'ng-click-outside';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MomentModule,
    RouterModule,
    SuiModule,
    ClickOutsideModule
  ],
  declarations: [
    ChatComponent,
    MiniChatComponent
  ],  
  exports: [
    ChatComponent,
    MiniChatComponent
  ],
  providers: [
    ChatService
  ]
})
export class ChatModule { }
