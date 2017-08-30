import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from "./chat.component";
import { FormsModule } from '@angular/forms'
import { MomentModule } from "angular2-moment";
import { ChatService } from "../services/chat/chat.service";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    MomentModule
  ],
  declarations: [
    ChatComponent
  ],
  exports: [
    ChatComponent
  ],
  providers: [
    ChatService
  ]
})
export class ChatModule { }
