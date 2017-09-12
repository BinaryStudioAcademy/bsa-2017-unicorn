import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class TaskMessagingService {

  private onTaskFinished = new Subject<void>();

  taskFinishedEvent = this.onTaskFinished.asObservable();

  constructor() { }

  finishTask() {
    this.onTaskFinished.next();
  }

}
