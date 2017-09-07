import { Component, OnInit, Input } from '@angular/core';
import { TokenHelperService } from "../services/helper/tokenhelper.service";
import { CalendarService } from "../services/calendar-service";
import { CalendarModel } from "../models/calendar/calendar";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
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
    this.calendarService.getCalendarByAccount(this.accountId)
    .then(res => {
      this.calendar = res;
      console.log(this.calendar);
    });
  }

}
