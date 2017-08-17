import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

import { Customer } from '../models/customer';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  @Input() social: firebase.User;

  @Input() public modal: SuiActiveModal<{}, {}, string>;

  mode: string;
  error = false;
  success = false;
  phone: string;
  birthday;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;

  constructor(private registerService: RegisterService,
    private router: Router) { }

  ngOnInit() {
    this.mode = 'date';
  }

  valid(): boolean {
    return this.birthday !== undefined && this.phone != undefined;
  }

  aggregateInfo(): Customer{
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

  confirmRegister() {
    if (this.valid()) {
      this.error = false;
      console.log('valid');
      let regInfo = this.aggregateInfo();
      console.log(regInfo);
      this.registerService.confirmCustomer(regInfo).then(resp => {
        this.modal.deny('');
        this.router.navigate(['user/1']);
      });
    } else {
      this.error = true;
    }
  }

}
