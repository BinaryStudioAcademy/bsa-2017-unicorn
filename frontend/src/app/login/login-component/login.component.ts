import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user = null;
  constructor(private auth: AuthService) { }

  ngOnInit() {
    this.auth.getAuthState().subscribe((user) => this.user = user);
  }

  loginWithGoogle() {
    this.auth.loginWithGoogle();
  }

  loginWithFacebook() {
    this.auth.loginWithFacebook().then(() => this.getUserData());
  }

  loginWithGithub() {
    this.auth.loginWithGithub().then(() => this.getUserData());
  }

  getUserData() {
    console.log(this.user);
  }

  logOut() {
    this.auth.logout();
  }

}
