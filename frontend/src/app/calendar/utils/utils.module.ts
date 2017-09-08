import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from "@angular/forms";
import { UtilsComponent } from './utils.component';
import { CalendarModule } from "angular-calendar/dist/esm/src";


@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CalendarModule
  ],
  declarations: [UtilsComponent],
  exports:[UtilsComponent]
})
export class UtilsModule { }