import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';

import { SuiActiveModal } from 'ng2-semantic-ui';
import { Company } from '../models/company';
import { HelperService } from '../../services/helper/helper.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';
import { Location } from '../../models/location.model'
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
  location: Location = new Location();
  constructor(private registerService: RegisterService,
    private helperService: HelperService,
    private authEventService: AuthenticationEventService) { }

  ngOnInit() {
    this.mode = 'date';
    this.email = this.social.email || null;
    this.phone = this.social.phoneNumber || null;
    this.name = this.social.displayName || null;
    this.location.Latitude = 49.841459;
    this.location.Longitude = 24.031946;
    this.location.City = "Lviv";
    this.location.PostIndex = "10";
    this.location.Adress = "Rynok";
  }

  aggregateInfo(): Company {
    return {
      foundation: this.foundation,
      staff: this.staff,
      description: this.description,
      phone: this.phone,
      email: this.email,
      image: this.social.photoURL,
      name: this.name,
      provider: this.social.providerData[0].providerId,
      uid: this.social.uid,
      location: this.location
    };
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
