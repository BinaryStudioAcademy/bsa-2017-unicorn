import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';

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
  speciality: string;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;

  mode: string;

  error: boolean = false;

  phone: string;
  birthday;

  constructor(private registerService: RegisterService) { }

  ngOnInit() {
    this.mode = 'date';
  }

  valid(): boolean {
    return this.birthday !== undefined && this.phone != undefined 
      && this.experience !== undefined && this.position !== undefined
      && this.speciality !== undefined;
  }

  aggregateInfo(): Vendor{
    let info = new Vendor();
    info.birthday = this.birthday;
    info.phone = this.phone;

    info.email = this.email;
    info.image = this.social.photoURL;
    info.firstName = this.firstName;
    info.middleName = this.middleName;
    info.lastName = this.lastName;
    info.provider = this.social.providerData[0].providerId;
    info.uid = this.social.uid;

    info.experience = this.experience;
    info.position = this.position;
    info.speciality = this.speciality;

    return info;
  }

  confirmRegister() {
    if (this.valid()) {
      this.error = false;
      console.log('valid');
      let regInfo = this.aggregateInfo();
      console.log(regInfo);
      this.registerService.confirmVendor(regInfo);
    } else {
      this.error = true;
    }
  }

}
