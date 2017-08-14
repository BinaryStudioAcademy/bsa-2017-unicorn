import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';

import { DataService } from '../data.service';
import { User } from './models/user';

@Injectable()
export class AuthService {
  constructor(public afAuth: AngularFireAuth, public httpService: DataService) {
  }

  private initializeUser(data): User {
    let user = new User();
    user.provider = data.providerData[0].providerId;
    user.uid = data.uid;

    return user;
  }

  private saveJWT(jwt: string) {
    if (jwt) {
      localStorage.setItem('token', jwt);
    }
  }

  public loginWithGoogle() {
    return this.afAuth.auth.signInWithPopup(new firebase.auth.GoogleAuthProvider())
      .then(data => {
        return this.initializeUser(data.user);
      })
      .then(user => {
        return this.httpService.postRequest('membership', user);
      });
  }

  public loginWithFacebook() {
    return this.afAuth.auth.signInWithPopup(new firebase.auth.FacebookAuthProvider())
      .then(data => {
        return this.initializeUser(data.user);
      })
      .then(user => {
        return this.httpService.postRequest('membership', user);
      });
  }

  public loginWithGithub() {
    return this.afAuth.auth.signInWithPopup(new firebase.auth.GithubAuthProvider())
      .then(data => {
        return this.initializeUser(data.user);
      })
      .then(user => {
        return this.httpService.postRequest('membership', user);
      });
  }

  public loginWithTwitter() {
    return this.afAuth.auth.signInWithPopup(new firebase.auth.TwitterAuthProvider())
      .then(data => {
        return this.initializeUser(data.user);
      })
      .then(user => {
        return this.httpService.postRequest('membership', user);
      });
  }

  public logout() {
    this.afAuth.auth.signOut();
    localStorage.removeItem('token');
  }
}