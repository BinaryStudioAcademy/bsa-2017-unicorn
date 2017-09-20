import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';

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
import { CalendarService } from "../../services/calendar-service";
import { CalendarModel } from "../../models/calendar/calendar";

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
  foundation: Date;

  loader: boolean;

  constructor(private registerService: RegisterService,
    private helperService: HelperService,
    private authEventService: AuthenticationEventService,
    public LocationService: LocationService,
    private apiLoader: NgMapAsyncApiLoader, 
    private tokenHelper: TokenHelperService,
    private companyService: CompanyService,
    private calendarService: CalendarService,
    private ref: ChangeDetectorRef) { }
  
    getCurrDate() { return new Date() }
  
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
    this.foundation.setDate(this.foundation.getDate() + 1);

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
        this.calendarService.createCalendar(+this.tokenHelper.getClaimByName('accountid'), this.createCalendar());
        this.helperService.redirectAfterAuthentication();
      }).catch(err => this.loader = false);
    }
  }
  placeChanged(event) {
    
    this.location.Latitude = event.geometry.location.lat();
    this.location.Longitude = event.geometry.location.lng()
    this.ref.detectChanges();
    this.LocationService.getLocDetails(this.location.Latitude,this.location.Longitude)
    .subscribe(
     result => {    
        this.location.Adress=(result.address_components[1].short_name+','+result.address_components[0].short_name)
         this.location.City=result.address_components[3].short_name;});
  }
  createCalendar():CalendarModel{
    return {
      Id: null,
      StartDate: new Date(),
      Events: null,
      EndDate: null,
      ExtraDayOffs: null,
      ExtraWorkDays: null,
      SeveralTasksPerDay: null,
      WorkOnWeekend: null
    }
  }

  // checkTheDate(date: Date):Date{    
  //     return new Date(date.setHours(date.getHours() - date.getTimezoneOffset() / 60));   
  // }
}
