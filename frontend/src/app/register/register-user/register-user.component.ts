import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';

import { SuiActiveModal } from 'ng2-semantic-ui';
import { Customer } from '../models/customer';
import { HelperService } from '../../services/helper/helper.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  @Input() social: firebase.User;
  @Input() public modal: SuiActiveModal<void, void, void>;

  mode: string;
  success = false;
  phone: string;
  birthday;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;

  constructor(private registerService: RegisterService,
    private helperService: HelperService,
    private authEventService: AuthenticationEventService) { }

  ngOnInit() {
    this.mode = 'date';
    this.email = this.social.email || null;
    this.phone = this.social.phoneNumber || null;
    this.firstName = this.social.providerData[0].displayName || null;
  }

  aggregateInfo(): Customer {
    let info = new Customer();
    info.birthday = this.birthday;

    info.phone = this.phone;
    info.email = this.email;
    info.image = this.social.photoURL;
    info.firstName = this.firstName;
    info.middleName = this.middleName;
    info.lastName = this.lastName;
    info.provider = this.social.providerData[0].providerId;
    info.uid = this.social.uid;

    return info;
  }

  confirmRegister(formData) {
    if (formData.valid) {
      let regInfo = this.aggregateInfo();
      this.registerService.confirmCustomer(regInfo).then(resp => {
        this.modal.deny(null);
        localStorage.setItem('token', resp.headers.get('token'));
        this.authEventService.signIn();
        this.helperService.redirectAfterAuthentication();
      });
    }
  }

}
