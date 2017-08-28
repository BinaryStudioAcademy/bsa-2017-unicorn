import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChatComponent } from "./chat.component";
import { FormsModule } from '@angular/forms'

@NgModule({
  imports: [
    CommonModule,
    FormsModule  
  ],
  declarations: [
    ChatComponent
  ],
  exports: [
    ChatComponent
  ]
})
export class ChatModule { }
