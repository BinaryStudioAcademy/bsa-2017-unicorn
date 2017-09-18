import { Injectable } from '@angular/core';

import { DataService } from './data.service';
import { CalendarModel } from "../models/calendar/calendar";

@Injectable()
export class CalendarService {

  constructor(private dataService: DataService) { 
    dataService.setHeader('Content-Type', 'application/json');
  }

  getCalendarByAccount(accountId: number):Promise<CalendarModel>{
    return this.dataService.getRequest<CalendarModel>("calendar/account/" + accountId);    
  }
  createCalendar(accountId: number, calendar: CalendarModel):Promise<CalendarModel>{
    return this.dataService.postRequest<CalendarModel>("calendar/"+ accountId, calendar); 
  }
  saveCalendar(calendar: CalendarModel):Promise<CalendarModel>{
    return this.dataService.postRequest<CalendarModel>("calendar/save", calendar); 
  }
}