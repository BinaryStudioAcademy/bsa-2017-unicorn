import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';

import * as firebase from 'firebase/app';
import { RegisterService } from '../../services/register.service';

import { SuiActiveModal } from 'ng2-semantic-ui';
import { Customer } from '../models/customer';
import { HelperService } from '../../services/helper/helper.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';
import { LocationModel } from '../../models/location.model'
import { LocationService } from "../../services/location.service";
import { NgMapAsyncApiLoader } from "@ngui/map/dist";

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
  styleUrls: ['./register-user.component.css']
})
export class RegisterUserComponent implements OnInit {

  @Input() social: firebase.User;
  @Input() public modal: SuiActiveModal<void, void, void>;
  @Input() location: LocationModel;
  
  mode: string;
  success = false;
  phone: string;
  birthday: Date;
  firstName: string;
  middleName: string;
  lastName: string;
  email: string; 

  loader: boolean;

  constructor(private registerService: RegisterService,
    private helperService: HelperService,
    private authEventService: AuthenticationEventService,
    private ref: ChangeDetectorRef,
    public LocationService: LocationService,
    private apiLoader: NgMapAsyncApiLoader) {
    
     }

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
    console.log(this.location);
    this.initName();
  }

  initName() {
    let displayName = this.social.displayName;
    let nameValues = displayName.split(' ');
    this.firstName = nameValues[0] || null;
    this.lastName = nameValues[1] || null;
  }

  aggregateInfo(): Customer {
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
      location: this.location
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
  getCurrDate() { return new Date() }

  confirmRegister(formData) {
    if (formData.valid) {
     
      let regInfo = this.aggregateInfo();
      this.loader = true;
      this.registerService.confirmCustomer(regInfo).then(resp => {
        this.loader = false;
        this.modal.deny(null);
        localStorage.setItem('token', resp.headers.get('token'));
        this.authEventService.signIn();
        this.helperService.redirectAfterAuthentication();
      }).catch(err => this.loader = false);
  }

}}
