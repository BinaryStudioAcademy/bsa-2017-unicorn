import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';

import { Vendor } from '../models/vendor';

@Component({
  selector: 'app-register-vendor',
  templateUrl: './register-vendor.component.html',
  styleUrls: ['./register-vendor.component.css']
})
export class RegisterVendorComponent implements OnInit {

  @Input() social: firebase.User;

  experience: number;
  position: string;
  company: string;
  speciality: string;

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
    return this.birthday !== undefined && this.gender != undefined && this.phone != undefined 
      && this.experience !== undefined && this.position !== undefined && this.company !== undefined
      && this.speciality !== undefined;
  }

  aggregateInfo(): Vendor{
    let info = new Vendor();
    info.birthday = this.birthday;
    info.gender = this.gender;
    info.phone = this.phone;

    info.email = this.social.email;
    info.image = this.social.photoURL;
    info.name = this.social.displayName;
    info.provider = this.social.providerData[0].providerId;
    info.uid = this.social.uid;

    info.experience = this.experience;
    info.position = this.position;
    info.company = this.company;
    info.speciality = this.speciality;

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
