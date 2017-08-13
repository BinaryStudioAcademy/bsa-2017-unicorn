import { Injectable } from '@angular/core';
import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class AuthService {
  private authState: Observable<firebase.User>
  private currentUser: firebase.User = null;

  constructor(public afAuth: AngularFireAuth) {
    this.authState = this.afAuth.authState;
    this.authState.subscribe(user => {
      if (user) {
        this.currentUser = user;
      } else {
        this.currentUser = null;
      }
    });
  }

  getAuthState() {
    return this.authState;
  }

  loginWithGoogle() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GoogleAuthProvider())
      .then(x => {
        // now we can work with user object (take uid, email etc.)
        // TODO: call service and get token from server
        console.log(x);
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
        console.log(x);
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithGithub() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GithubAuthProvider())
      .then(x => {
        console.log(x);
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithTwitter() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.TwitterAuthProvider())
      .then(x => {
        console.log(x);
      })
      .catch(err => {
        alert(err);
      });
  }

  isLoggenIn() {
    return this.currentUser == null ? false : true;
  }

  logout() {
    this.afAuth.auth.signOut();
  }
}