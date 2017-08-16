import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';

import { DataService } from '../data.service';
import { UserAuth } from '../../models/userauth';

@Injectable()
export class AuthService {  
  public authState: Observable<firebase.User>
  private apiController:string;

  constructor(public afAuth: AngularFireAuth, public httpService: DataService) {
    httpService.setHeader('Content-Type', 'application/json');
    this.authState = this.afAuth.authState;    
    this.apiController = 'membership';
  }

  public initializeUser(data): UserAuth {
    return {
      provider: data.providerData[0].providerId,     
      uid: data.uid
    }
  }  

  public loginWithGoogle() {
    return this.afAuth.auth.signInWithPopup(new firebase.auth.GoogleAuthProvider())
      .then(data => {
        console.log(data);
        return this.initializeUser(data.user);
      })
      .then(user => {
        return this.httpService.postFullRequest(this.apiController, user);
      });
  }

  public loginWithFacebook() {
    return this.afAuth.auth.signInWithPopup(new firebase.auth.FacebookAuthProvider())
      .then(data => {
        console.log(data);
        return this.initializeUser(data.user);
      })
      .then(user => {
        return this.httpService.postFullRequest(this.apiController, user);
      });
  }

  public loginWithTwitter() {
    return this.afAuth.auth.signInWithPopup(new firebase.auth.TwitterAuthProvider())
      .then(data => {
        console.log(data);
        return this.initializeUser(data.user);
      })
      .then(user => {
        return this.httpService.postFullRequest(this.apiController, user);
      });
  }

  public saveJwt(token: string) {
    if (token) {
      localStorage.setItem('token', token);
    }
  }

  public logout() {
    localStorage.removeItem('token');
    this.afAuth.auth.signOut();
  }
}