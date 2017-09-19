import { Component, ViewChild, TemplateRef, OnInit, Input, NgZone } from '@angular/core';
import { startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours } from 'date-fns';
import { Subject } from 'rxjs/Subject';
import { CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarMonthViewDay } from 'angular-calendar';
import { CalendarModel } from "../models/calendar/calendar";
import { TokenHelperService } from "../services/helper/tokenhelper.service";
import { CalendarService } from "../services/calendar-service";
import { SuiModalService, ModalTemplate, TemplateModalConfig, ModalSize, SuiActiveModal } from "ng2-semantic-ui";
import { VendorBook } from "../models/book/vendor-book.model";
import { BookStatus } from "../models/book/book.model";
import { CalendarEventsService } from "../services/events/calendar-events.service";
import { Subscription } from "rxjs/Subscription";
import { WeekDay } from "calendar-utils/dist/calendar-utils";
import { NotificationService } from "../services/notifications/notification.service";

export interface IDayContext {
  date: Date,
  events: CalendarEvent[],
  day: any
}

export interface ISettingsContext {
  startDate: Date,
  endDate: Date,
  workOnWeekend: boolean,
  severalTasksPerDay: boolean
}

const colors: any = { 
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  }
};

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',  
  styleUrls: ['./calendar.component.sass']
})
export class CalendarComponent implements OnInit {
  @ViewChild('dayModalTemplate')
  public dayModalTemplate:ModalTemplate<IDayContext, void, void>

  @ViewChild('settingsModalTemplate')
  public settingsModalTemplate:ModalTemplate<ISettingsContext, void, void>

  private activeModal: SuiActiveModal<IDayContext, {}, void>;

  @Input()
  accountId: number;

  calendarModel: CalendarModel;  
  events: CalendarEvent[] = [];
  isLoading: boolean = true;
  wasWeekend: boolean;
  isSavingWeekend: boolean = false;
  isChangedWorktime: boolean = false;
  weekendDays: number[] = [];
  settingsClicked: Subscription;

  view: string = 'month';  
  viewDate: Date = new Date();    

  refresh: Subject<any> = new Subject();

  constructor(
    private tokenHelper: TokenHelperService,
    private calendarService: CalendarService,
    private modalService: SuiModalService,
    private calendarEventsService: CalendarEventsService,
    private notificationService: NotificationService) { }

  ngOnInit() {   
    this.settingsClicked = this.calendarEventsService.settingsClickEvent$.subscribe(() => {      
      this.openSettingsModal();
    })

    this.notificationService.listen<VendorBook>("RefreshCalendarsEvents", res => {   
      this.events.push({
        start: new Date(res.Date),
        title: res.Work.Name,
        end: new Date(res.EndDate),        
        color: colors.blue,
        meta: {
          status: BookStatus[res.Status],
          description: res.Description,
          customer: res.Customer,
          customerPhone: res.CustomerPhone,
          workIcon: res.Work.Icon
        }
      }); 
      this.refresh.next();       
    });
    

    this.calendarService.getCalendarByAccount(this.accountId)
    .then(res => {   
      this.calendarModel = res;
      if(this.calendarModel.WorkOnWeekend){
        this.weekendDays = [];
      }
      else{
        this.weekendDays = [0,6];
      }            
      if(this.calendarModel.Events){        
        this.calendarModel.Events.forEach(event => {
          if(event.Status === BookStatus.InProgress || event.Status === BookStatus.Accepted){
          this.events.push({
              start: new Date(event.Date),
              title: event.Work.Name,
              end: new Date(event.EndDate),        
              color: colors.blue,
              meta: {
                status: BookStatus[event.Status],
                description: event.Description,
                customer: event.Customer,
                customerPhone: event.CustomerPhone,
                workIcon: event.Work.Icon
              }
            });     
          }       
        });
      }
      this.isLoading = false; 
    });
  }
  
  dayClicked(day: any): void {     
    if (isSameMonth(day.date, this.viewDate)) {      
      this.openDayModal(day, day.events);      
    }
  }

  weekDayClicked(day: any){    
    let events = this.events.filter(x => day.date.toLocaleDateString() >= x.start.toLocaleDateString() && day.date.toLocaleDateString() <= x.end.toLocaleDateString() );
    this.openDayModal(day, events);
  }

  openDayModal(day: any, events: CalendarEvent[]) {
    this.wasWeekend = day.isWeekend;
    const config = new TemplateModalConfig<IDayContext, void, void>(this.dayModalTemplate);
    config.context = {date:day.date, events:events, day: day};
    config.isInverted = true;
    config.size = ModalSize.Normal;
    this.activeModal = this.modalService.open(config);    
  }

  openSettingsModal(){
    const config = new TemplateModalConfig<ISettingsContext, void, void>(this.settingsModalTemplate);
    config.context = {
      startDate: new Date(this.calendarModel.StartDate),
      endDate: this.calendarModel.EndDate !== null ? new Date(this.calendarModel.EndDate): null,
      workOnWeekend: this.calendarModel.WorkOnWeekend,
      severalTasksPerDay: this.calendarModel.SeveralTasksPerDay
    };
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.activeModal = this.modalService.open(config);    
  }

  closeSettingsModal(context: any){    
    this.isChangedWorktime = true;
    if(context.startDate){
    this.calendarModel.StartDate = new Date(context.startDate);
    }    
    if(context.endDate){
      this.calendarModel.EndDate = new Date(context.endDate);
    }
    else{
      this.calendarModel.EndDate = null;
    }
    this.calendarModel.WorkOnWeekend = context.workOnWeekend;
    this.calendarModel.SeveralTasksPerDay = context.severalTasksPerDay;
    
    if(context.workOnWeekend){
      this.calendarModel.ExtraWorkDays = [];
      this.calendarModel.ExtraDayOffs = this.calendarModel.ExtraDayOffs.filter(x => new Date(x.Day).getDay() !== 6 && new Date(x.Day).getDay() !== 0);
      this.weekendDays = [];
    }
    else{
      this.weekendDays = [0,6];
    }
    this.calendarService.saveCalendar(this.calendarModel).then(() => {
      this.isChangedWorktime = false;
      this.activeModal.deny(null);     
    }); 
  }

  closeDayModal(day: any){     
    if(day.isWeekend !== this.wasWeekend){
      this.isSavingWeekend = true;      
      // let _currentDate = this.checkTheDate(new Date(day.date)).toJSON(); 
      if(day.isWeekend && ((day.date.getDay() !== 6 && day.date.getDay() !== 0) ||
        ((day.date.getDay() === 6 || day.date.getDay() === 0) && this.calendarModel.WorkOnWeekend))){
        this.calendarModel.ExtraDayOffs.push({
          Id: null,
          CalendarId: this.calendarModel.Id,
          Day: new Date(day.date),
          DayOff: true
        });
      }      
      else if(!day.isWeekend && (day.date.getDay() === 6 || day.date.getDay() === 0) && !this.calendarModel.WorkOnWeekend){        
        this.calendarModel.ExtraWorkDays.push({
          Id: null,
          CalendarId: this.calendarModel.Id,
          Day: new Date(day.date),
          DayOff: false
        });        
      }
      else if(!day.isWeekend && (day.date.getDay() === 6 || day.date.getDay() === 0) && this.calendarModel.WorkOnWeekend){   
        this.calendarModel.ExtraDayOffs = this.calendarModel.ExtraDayOffs.filter(x => new Date(x.Day).toLocaleDateString() !== day.date.toLocaleDateString());
      }
      else if(day.isWeekend && (day.date.getDay() === 6 || day.date.getDay() === 0) && !this.calendarModel.WorkOnWeekend){
        this.calendarModel.ExtraWorkDays = this.calendarModel.ExtraWorkDays.filter(x => new Date(x.Day).toLocaleDateString() !== day.date.toLocaleDateString());
      }
      else if(!day.isWeekend && (day.date.getDay() !== 6 && day.date.getDay() !== 0)){
        this.calendarModel.ExtraDayOffs = this.calendarModel.ExtraDayOffs.filter(x => new Date(x.Day).toLocaleDateString() !== day.date.toLocaleDateString());
      }      
      this.calendarService.saveCalendar(this.calendarModel).then(() => {
        this.isSavingWeekend = false;
        this.activeModal.deny(null);
      }); 
    }
    else{
      this.activeModal.deny(null); 
    }        
    this.wasWeekend = undefined;   
  }

  beforeMonthViewRender({ body }: { body: CalendarMonthViewDay[] }): void {       
    this.render(body);
  }

  beforeWeekViewRender({ header }: { header: WeekDay[] }): void { 
    this.render(header);
  }

  render(mas: any[]){    
    mas.forEach(day => {
      // let _currentDate = this.checkTheDate(new Date(day.date)).toJSON(); 
      if(this.calendarModel.ExtraWorkDays.find(x => new Date(x.Day).toLocaleDateString() == day.date.toLocaleDateString())){        
        day.isWeekend = false;
      }
      else if(this.calendarModel.ExtraDayOffs.find(x => new Date(x.Day).toLocaleDateString() == day.date.toLocaleDateString())){
        day.isWeekend = true;
      }  
    });
  }

  ngOnDestroy() {
    this.settingsClicked.unsubscribe();
  }

}
