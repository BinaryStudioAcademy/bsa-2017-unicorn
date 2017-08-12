import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';

import { Customer } from '../models/customer';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  @Input() social: firebase.User;

  mode: string;
  error: boolean = false;
  phone: string;
  birthday;
  gender: string;

  constructor() { }

  ngOnInit() {
    this.mode = 'date';
  }

  valid(): boolean {
    return this.birthday !== undefined && this.gender != undefined && this.phone != undefined;
  }

  aggregateInfo(): Customer{
    let info = new Customer();
    info.birthday = this.birthday;
    info.gender = this.gender;
    
    info.phone = this.phone;
    info.email = this.social.email;
    info.image = this.social.photoURL;
    info.name = this.social.displayName;
    info.provider = this.social.providerData[0].providerId;
    info.uid = this.social.uid;

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
