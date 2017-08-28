import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class DashMessagingService {
  private onPendingChanged = new Subject<void>();
  private onProgressChanged = new Subject<void>();

  progressEvent$ = this.onProgressChanged.asObservable();
  pendingEvent$ = this.onPendingChanged.asObservable();

  constructor() { }

  changePending() {
    this.onPendingChanged.next();
  }

  changeProgress() {
    this.onProgressChanged.next();
  }

}
