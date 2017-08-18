import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class LoginService {
  private _source = new Subject<boolean>();
  loginEvent$ = this._source.asObservable();

  login(state:boolean) {    
    this._source.next(state);
  }

}
