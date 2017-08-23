import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EnumKeysPipe } from "./pipes/enum.pipe";

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    EnumKeysPipe
  ],
  exports: [EnumKeysPipe]
})
export class PipeModule {
  
 }
