import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  constructor(private auth: AuthService) { }

  ngOnInit() {
  }

  loginWithGoogle() {
    this.auth.loginWithGoogle();
  }

  loginWithFacebook() {
    this.auth.loginWithFacebook()
      .then(user => {
        console.log(user);
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithGithub() {
    this.auth.loginWithGithub();
  }

  loginWithTwitter() {
    this.auth.loginWithTwitter();
  }

  logOut() {
    this.auth.logout();
  }

}
