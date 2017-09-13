import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class CalendarEventsService {
  private _onSettingsClick = new Subject<void>();    

  settingsClickEvent$ = this._onSettingsClick.asObservable();     

  clickedSettings() {
    this._onSettingsClick.next();
  }
}