import { Component, OnInit, Input } from '@angular/core';

import { Company } from '../models/company';

@Component({
  selector: 'app-register-company',
  templateUrl: './register-company.component.html',
  styleUrls: ['./register-company.component.css']
})
export class RegisterCompanyComponent implements OnInit {

  @Input() social: any;

  mode: string;
  error: boolean = false;
  phone: string;
  description: string;
  staff: number;
  foundation: any;

  constructor() { }

  ngOnInit() {
    this.mode = 'date';
  }

  valid(): boolean {
    return this.foundation !== undefined && this.staff != undefined && this.phone != undefined
      && this.description !== undefined;
  }

  aggregateInfo(): Company {
    let info = new Company();
    info.foundation = this.foundation;
    info.staff = this.staff;
    info.description = this.description;
    info.phone = this.phone;
    info.email = this.social.email;
    info.image = this.social.image;
    info.name = this.social.name;
    info.provider = this.social.provider;
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
