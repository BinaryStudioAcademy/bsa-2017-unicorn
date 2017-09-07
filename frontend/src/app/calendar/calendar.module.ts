import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CalendarComponent } from './calendar.component';
import { FormsModule } from '@angular/forms';
import { CalendarModule } from 'angular-calendar';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    CalendarModule.forRoot(),
  ],
  declarations: [CalendarComponent], 
  exports: [CalendarComponent]
})
export class OwnCalendarModule { }
