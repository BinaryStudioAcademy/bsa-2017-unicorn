import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class AuthenticationEventService {
  private _onLogIn = new Subject<void>();
  private _onLogOut = new Subject<void>();

  loginEvent$ = this._onLogIn.asObservable();
  logoutEvent$ = this._onLogOut.asObservable();

  signIn() {
    this._onLogIn.next();
  }

  signOut() {
    this._onLogOut.next();
  }

}
