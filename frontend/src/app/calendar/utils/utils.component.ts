import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { CalendarEventsService } from "../../services/events/calendar-events.service";

@Component({
  selector: 'mwl-utils-calendar-header',
  templateUrl: './utils.component.html',
  styleUrls: ['./utils.component.sass']
})
export class UtilsComponent {
  @Input() view: string;
  
  @Input() viewDate: Date;

  @Input() locale: string = 'en';

  @Output() viewChange: EventEmitter<string> = new EventEmitter();

  @Output() viewDateChange: EventEmitter<Date> = new EventEmitter();

  constructor(private calendarEventsService: CalendarEventsService){
    
  }

  onClick(){
    this.calendarEventsService.clickedSettings();
  }
}
