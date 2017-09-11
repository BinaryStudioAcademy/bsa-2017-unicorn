import {
  Component,
  ChangeDetectionStrategy,
  ViewChild,
  TemplateRef,
  OnInit,
  Input
} from '@angular/core';
import {
  startOfDay,
  endOfDay,
  subDays,
  addDays,
  endOfMonth,
  isSameDay,
  isSameMonth,
  addHours
} from 'date-fns';
import { Subject } from 'rxjs/Subject';
import {
  CalendarEvent,
  CalendarEventAction,
  CalendarEventTimesChangedEvent,
  CalendarMonthViewDay
} from 'angular-calendar';
import { CalendarModel } from "../models/calendar/calendar";
import { TokenHelperService } from "../services/helper/tokenhelper.service";
import { CalendarService } from "../services/calendar-service";
import { SuiModalService, ModalTemplate, TemplateModalConfig, ModalSize } from "ng2-semantic-ui";

export interface IReviewContext {
  date: Date,
  events: CalendarEvent[]
}

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
@ViewChild('modalTemplate')
public modalTemplate:ModalTemplate<IReviewContext, void, void>

@Input()
accountId: number;

calendar: CalendarModel;


  constructor(
    private tokenHelper: TokenHelperService,
    private calendarService: CalendarService,
    private modalService: SuiModalService,) { }

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
      start: subDays(startOfDay(new Date()), 1),
      end: addDays(new Date(), 1),
      title: 'A 3 day event',
      color: colors.blue,        
    },
    {
      start: startOfDay(new Date()),
      title: 'An event with no end date',
      color: colors.blue,        
    },
    {
      start: subDays(endOfMonth(new Date()), 3),
      end: addDays(endOfMonth(new Date()), 3),
      title: 'A long event that spans 2 months',
      color: colors.blue
    },
    {
      start: addHours(startOfDay(new Date()), 2),
      end: new Date(),
      title: 'A draggable and resizable event',
      color: colors.blue,        
      resizable: {
        beforeStart: true,
        afterEnd: true
      },
      draggable: true
    }
  ];

  refresh: Subject<any> = new Subject();

  dayClicked(event: any, day: any): void {  
    if (isSameMonth(day.date, this.viewDate)) {
      this.openModal(day);
    }
  }

  eventTimesChanged({
    event,
    newStart,
    newEnd
  }: CalendarEventTimesChangedEvent): void {
    event.start = newStart;
    event.end = newEnd;      
    this.refresh.next();
  }


  openModal(day: any) {
    const config = new TemplateModalConfig<IReviewContext, void, void>(this.modalTemplate);
    config.context = {date:day.date, events:day.events};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.modalService.open(config);
  }
}
