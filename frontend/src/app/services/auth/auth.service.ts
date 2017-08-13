import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';

import { DataService } from '../data.service';
import { User } from './models/user';

@Injectable()
export class AuthService {
  private user: User = new User();

  constructor(public afAuth: AngularFireAuth) {
    afAuth.authState.subscribe(user => {
      if (user) {
        // get user providerId and uid
        this.initializeUser(user);
        console.log(this.user);

        // Call backend
        // TODO: call backend
      }
    });
  }

  initializeUser(data) {
    this.user.provider = data.providerData[0].providerId;
    this.user.uid = data.uid;
  }

  loginWithGoogle() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GoogleAuthProvider())
      .catch(err => {
        // This prevent error in console, we can handle it there (user close popup error)
        // TODO: show message box(modal) like airbnb
        alert(err);
      });
  }

  loginWithFacebook() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.FacebookAuthProvider())
      .catch(err => {
        alert(err);
      });
  }

  loginWithGithub() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GithubAuthProvider())
      .catch(err => {
        alert(err);
      });
  }

  loginWithTwitter() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.TwitterAuthProvider())
      .catch(err => {
        alert(err);
      });
  }

  logout() {
    this.afAuth.auth.signOut();
    localStorage.removeItem('token');
  }

  saveJWT(jwt: string) {
    if (jwt) {
      localStorage.setItem('token', jwt);
    }
  }
}