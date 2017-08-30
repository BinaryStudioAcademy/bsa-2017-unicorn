import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EnumKeysPipe } from "./pipes/enum.pipe";
import { ViewedNotificationsPipe } from "./pipes/viewed-notifications.pipe";

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    EnumKeysPipe,
    ViewedNotificationsPipe
  ],
  exports: [
    EnumKeysPipe,
    ViewedNotificationsPipe
  ]
})
export class PipeModule {
  
 }
