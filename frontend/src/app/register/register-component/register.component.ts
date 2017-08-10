import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { AuthService } from 'angular2-social-login';

import { RegisterInfo } from '../models/register-info';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit, OnDestroy {

  mode: 'date';

  public user;
  sub: any;
  isLogged: boolean = false;
  error: boolean = false;

  phone: string;
  birthday;
  gender: string;
  options = ['Male', 'Female'];

  constructor(
    private auth: AuthService,
    private location: Location,
    private router: Router) { }

  ngOnInit() {
  }

  ngOnDestroy() {
    //this.sub.unsubscribe();
  }

  register(provider: string) {
    this.sub = this.auth.login(provider).subscribe(
      (data) => {
        console.log(data);
        this.user=data;
        this.isLogged = true;
      }
    )
  }

  logout() {
    this.auth.logout().subscribe(
      (data) => {
        console.log(data);
        this.user=null;
        this.isLogged = false;
      }
    )
  }

  valid(): boolean {
    return this.birthday !== undefined && this.gender != undefined && this.phone != undefined;
  }

  aggregateInfo(): RegisterInfo{
    let info = new RegisterInfo();
    info.birthday = this.birthday;
    info.gender = this.gender;
    info.phone = this.phone;
    info.email = this.user.email;
    info.image = this.user.image;
    info.name = this.user.name;
    info.provider = this.user.provider;
    info.uid = this.user.uid;

    return info;
  }

  confirmRegister() {
    if (this.valid()) {
      this.error = false;
      console.log('valid');
      let regInfo = this.aggregateInfo();
      console.log(regInfo);
    } else {
      this.error = true;
    }
  }

}
