import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';
import { SuiActiveModal } from 'ng2-semantic-ui';
import { Vendor } from '../models/vendor';
import { HelperService } from '../../services/helper/helper.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';
import { LocationModel } from '../../models/location.model'
import { LocationService } from "../../services/location.service";
import { NgMapAsyncApiLoader } from "@ngui/map/dist";
import { TokenHelperService } from "../../services/helper/tokenhelper.service";
import { VendorService } from "../../services/vendor.service";
import { Contact } from "../../models/contact.model";
import { CalendarService } from "../../services/calendar-service";
import { CalendarModel } from "../../models/calendar/calendar";


@Component({
  selector: 'app-register-vendor',
  templateUrl: './register-vendor.component.html',
  styleUrls: ['./register-vendor.component.css']
})
export class RegisterVendorComponent implements OnInit {

  @Input() social: firebase.User;
  @Input() public modal: SuiActiveModal<void, void, void>;
  @Input() location: LocationModel;

  experience: number;
  position: string;
  speciality: string;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string;
  mode: string;

  phone: string;
  birthday: Date;

  loader: boolean;

  constructor(private registerService: RegisterService,
    private helperService: HelperService,
    private authEventService: AuthenticationEventService,
    public LocationService: LocationService,
    private tokenHelper: TokenHelperService,
    private apiLoader: NgMapAsyncApiLoader,
    private vendorService: VendorService,
    private calendarService: CalendarService,
    private ref: ChangeDetectorRef    
  ) { }
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
    this.initName();
  }

  initName() {
    let displayName = this.social.displayName;
    let nameValues = displayName.split(' ');
    this.firstName = nameValues[0] || null;
    this.lastName = nameValues[1] || null;
    
  }

  aggregateInfo(): Vendor {
    this.birthday.setDate(this.birthday.getDate() + 1);

    return {
      birthday: this.birthday,
      phone: this.phone,
      email: this.email,
      image: this.social.photoURL,
      firstName: this.firstName,
      middleName: this.middleName,
      lastName: this.lastName,
      provider: this.social.providerData[0].providerId,
      uid: this.social.uid,
      experience: this.experience,
      position: this.position,
      speciality: this.speciality,
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
      this.registerService.confirmVendor(regInfo)
        .then(resp => {
          this.loader = false;
          this.modal.deny(null);
          localStorage.setItem('token', resp.headers.get('token'));
          this.vendorService.postVendorContact(+this.tokenHelper.getClaimByName('profileid'),emailContact);
          this.vendorService.postVendorContact(+this.tokenHelper.getClaimByName('profileid'),phoneContact);
          this.authEventService.signIn();
          this.calendarService.createCalendar(+this.tokenHelper.getClaimByName('accountid'), this.createCalendar());
          this.helperService.redirectAfterAuthentication();
        })
        .catch(err => this.loader = false);
    }
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
}
