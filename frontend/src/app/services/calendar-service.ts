import { Injectable } from '@angular/core';

import { DataService } from './data.service';
import { CalendarModel } from "../models/calendar/calendar";

@Injectable()
export class CalendarService {

  constructor(private dataService: DataService) { 
    dataService.setHeader('Content-Type', 'application/json');
  }

  getCalendar(calendarId: number):Promise<CalendarModel>{
    return this.dataService.getRequest<CalendarModel>("calendar/" + calendarId);    
  }
  createCalendar(accountId: number):Promise<CalendarModel>{
    return this.dataService.postRequest<CalendarModel>("calendar", accountId); 
  }
  saveCalendar(calendar: CalendarModel):Promise<CalendarModel>{
    return this.dataService.postRequest<CalendarModel>("calendar/save", calendar); 
  }
}