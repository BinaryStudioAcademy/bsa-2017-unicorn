import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from "./chat.component";
import { FormsModule } from '@angular/forms'
import { MomentModule } from "angular2-moment";

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
  ]
})
export class ChatModule { }
