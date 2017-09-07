import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
  OnInit,
  Input
} from '@angular/core';

import { Subject } from 'rxjs/Subject';

import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent
} from 'angular-calendar';
import { CalendarModel } from "../models/calendar/calendar";
import { TokenHelperService } from "../services/helper/tokenhelper.service";
import { CalendarService } from "../services/calendar-service";

const colors: any = {
  red: {
    primary: '#ad2121',
    secondary: '#FAE3E3'
  },
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  },
  yellow: {
    primary: '#e3bc08',
    secondary: '#FDF1BA'
  }
};

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  styleUrls: ['./calendar.component.sass']
})
export class CalendarComponent implements OnInit {

@Input()
accountId: number;

calendar: CalendarModel;


  constructor(
    private tokenHelper: TokenHelperService,
    private calendarService: CalendarService) { }

  ngOnInit() {
    // this.calendarService.getCalendarByAccount(this.accountId)
    // .then(res => {
    //   this.calendar = res;
    //   console.log(this.calendar);
    // });
  }

  view: string = 'month';
  
    viewDate: Date = new Date();
  
    events: CalendarEvent[] = [
      {
        title: 'Draggable event',
        color: colors.yellow,
        start: new Date(),
        draggable: true
      },
      {
        title: 'A non draggable event',
        color: colors.blue,
        start: new Date()
      }
    ];
  
    refresh: Subject<any> = new Subject();
  
    eventTimesChanged({
      event,
      newStart,
      newEnd
    }: CalendarEventTimesChangedEvent): void {
      event.start = newStart;
      event.end = newEnd;
      this.refresh.next();
    }

}
