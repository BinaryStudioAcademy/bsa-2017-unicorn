import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';

import { SuiActiveModal } from 'ng2-semantic-ui';
import { Company } from '../models/company';
import { HelperService } from '../../services/helper/helper.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';

@Component({
  selector: 'app-register-company',
  templateUrl: './register-company.component.html',
  styleUrls: ['./register-company.component.css']
})
export class RegisterCompanyComponent implements OnInit {

  @Input() social: firebase.User;
  @Input() public modal: SuiActiveModal<void, void, void>;

  name: string;
  mode: string;
  phone: string;
  description: string;
  staff: number;
  email: string;
  foundation: any;

  constructor(private registerService: RegisterService,
    private helperService: HelperService,
    private authEventService: AuthenticationEventService) { }

  ngOnInit() {
    this.mode = 'date';
    this.email = this.social.email || null;
    this.phone = this.social.phoneNumber || null;
    this.name = this.social.displayName || null;
  }

  aggregateInfo(): Company {
    let info = new Company();
    info.foundation = this.foundation;
    info.staff = this.staff;
    info.description = this.description;
    info.phone = this.phone;
    info.email = this.email;
    info.image = this.social.photoURL;
    info.name = this.name;
    info.provider = this.social.providerData[0].providerId;
    info.uid = this.social.uid;

    return info;
  }

  confirmRegister(formData) {
    if (formData.valid) {
      let regInfo = this.aggregateInfo();
      this.registerService.confirmCompany(regInfo).then(resp => {
        this.modal.deny(null);
        localStorage.setItem('token', resp.headers.get('token'));
        this.authEventService.signIn();
        this.helperService.redirectAfterAuthentication();
      });
    }
  }
}
