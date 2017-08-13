import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';

import { DataService } from '../data.service';
import { User } from './models/user';

@Injectable()
export class AuthService {
  public user: User = new User();

  constructor(public afAuth: AngularFireAuth, public httpService: DataService) {
  }

  initializeUser(data) {
    this.user.provider = data.providerId;
    this.user.uid = data.uid;
  }

  loginWithGoogle() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GoogleAuthProvider())
      .then(x => {
        // TODO: rewrite with async ???
        // TODO: POST query to server for get a token
        this.initializeUser(x.user.providerData[0]); // use data from original provider (google, twitter etc.)
        console.log(this.user);

        this.httpService.postRequest('membership', this.user).then(x => {
          /* TODO: check status code from server
          404 - invalid params (provider or uid) => modal with error or log
          204 - user not found (special for register) => redirect to register page
          200 - OK (header Token) => get token from header => saveJWT(token) => redirect to cabinet / profile (login succesfully)
          */
        });

      })
      .catch(err => {
        // This prevent error in console, we can handle it there (user close popup error)
        alert(err);
      });
  }

  loginWithFacebook() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.FacebookAuthProvider())
      .then(x => {
        this.initializeUser(x.user.providerData[0]);
        console.log(this.user);
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithGithub() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GithubAuthProvider())
      .then(x => {
        this.initializeUser(x.user.providerData[0]);
        console.log(this.user);
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithTwitter() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.TwitterAuthProvider())
      .then(x => {
        this.initializeUser(x.user.providerData[0]);
        console.log(this.user);
      })
      .catch(err => {
        alert(err);
      });
  }

  logout() {
    this.afAuth.auth.signOut();
  }

  saveJWT(jwt: string) {
    if (jwt) {
      localStorage.setItem('token', jwt);
    }
  }
}