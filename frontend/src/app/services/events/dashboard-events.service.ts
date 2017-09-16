import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class DashboardEventsService {
  private _onChangeStatusToFinished = new Subject<void>();    

  changeStatusToFinishedEvent$ = this._onChangeStatusToFinished.asObservable();  

  changeStatusToFinished() {
    this._onChangeStatusToFinished.next();
  }
}