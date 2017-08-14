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

  private saveJWT(jwt: string) {
    if (jwt) {
      localStorage.setItem('token', jwt);
    }
  }

  loginWithGoogle() {
    this.auth.loginWithGoogle()
      .then(resp => {
        if (resp.status == 200) {
          this.saveJWT(resp.headers.get('Token'));
        }
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithFacebook() {
    this.auth.loginWithFacebook()
      .then(resp => {
        if (resp.status == 200) {
          this.saveJWT(resp.headers.get('Token'));
        }
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithGithub() {
    this.auth.loginWithGithub()
      .then(resp => {
        if (resp.status == 200) {
          this.saveJWT(resp.headers.get('Token'));
        }
      })
      .catch(err => {
        alert(err);
      });
  }

  loginWithTwitter() {
    this.auth.loginWithTwitter()
      .then(resp => {
        if (resp.status == 200) {
          this.saveJWT(resp.headers.get('Token'));
        }
      })
      .catch(err => {
        alert(err);
      });
  }

  logOut() {
    localStorage.removeItem('token');
    this.auth.logout();
  }

}
