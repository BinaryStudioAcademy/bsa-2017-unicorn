import { Component, OnInit, Input } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';

import { SuiActiveModal } from 'ng2-semantic-ui';
import { Company } from '../models/company';
import { HelperService } from '../../services/helper/helper.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';
import { LocationModel } from '../../models/location.model'
import { LocationService } from "../../services/location.service";
import { NgMapAsyncApiLoader } from "@ngui/map/dist";
import { Contact } from "../../models/contact.model";
import { TokenHelperService } from "../../services/helper/tokenhelper.service";
import { CompanyService } from "../../services/company-services/company.service";

@Component({
  selector: 'app-register-company',
  templateUrl: './register-company.component.html',
  styleUrls: ['./register-company.component.css']
})
export class RegisterCompanyComponent implements OnInit {

  @Input() social: firebase.User;
  @Input() public modal: SuiActiveModal<void, void, void>;
  @Input() location: LocationModel;
  name: string;
  mode: string;
  phone: string;
  description: string;
  staff: number;
  email: string;
  foundation: any;

  loader: boolean;

  constructor(private registerService: RegisterService,
    private helperService: HelperService,
    private authEventService: AuthenticationEventService,
    public LocationService: LocationService,
    private apiLoader: NgMapAsyncApiLoader, 
    private tokenHelper: TokenHelperService,
    private companyService: CompanyService) { }

  ngOnInit() {
    this.apiLoader.load();
    this.LocationService.getGoogle().then((g) => {
      this.LocationService.getLocDetails(this.location.Latitude, this.location.Longitude).subscribe(
        result => {
          this.location.Adress=(result.address_components[1].short_name+','+result.address_components[0].short_name)
          this.location.City=result.address_components[3].short_name;
        }
      );
    });
    this.mode = 'date';
    this.email = this.social.email || null;
    this.phone = this.social.phoneNumber || null;
    this.name = this.social.displayName || null;
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
      this.loader = true;
      var emailContact = new Contact();
      emailContact.Provider = "email";
      emailContact.ProviderId = 2;
      emailContact.Type = "Email";
      emailContact.Value = regInfo.email;

      var phoneContact = new Contact();
      phoneContact.Provider = "phone";
      phoneContact.ProviderId = 1;
      phoneContact.Type = "Phone";
      phoneContact.Value = regInfo.phone;
      
      this.registerService.confirmCompany(regInfo).then(resp => {
        this.loader = false;
        this.modal.deny(null);
        localStorage.setItem('token', resp.headers.get('token'));
        this.companyService.addCompanyContact(+this.tokenHelper.getClaimByName('profileid'),emailContact);
        this.companyService.addCompanyContact(+this.tokenHelper.getClaimByName('profileid'),phoneContact);
        this.authEventService.signIn();
        this.helperService.redirectAfterAuthentication();
      }).catch(err => this.loader = false);
    }
  }
}
