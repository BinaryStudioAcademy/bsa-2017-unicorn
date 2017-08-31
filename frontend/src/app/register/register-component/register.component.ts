import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { Location } from '@angular/common';

import { AuthenticationLoginService } from '../../services/auth/authenticationlogin.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';

import * as firebase from 'firebase/app';

import { HelperService } from '../../services/helper/helper.service';

import { RegisterService } from '../../services/register.service';
import { NguiMapModule, NgMapApiLoader, NgMapAsyncApiLoader, NguiMap } from "@ngui/map";
import { ComponentModalConfig, ModalSize, SuiModal } from 'ng2-semantic-ui';
import { LocationService } from "../../services/location.service";
import { LocationModel } from "../../models/location.model";

export class RegisterModal extends ComponentModalConfig<void> {
  constructor() {
    super(RegisterComponent);
    this.size = ModalSize.Small;
    this.isInverted = true;
  }
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService, NgMapAsyncApiLoader]
})
export class RegisterComponent implements OnInit, OnDestroy {
  private currentUser: firebase.User = null;
  mode: string;
  error: boolean = false;

  public roles: { [role: string]: boolean } = {};

  social: any;

  isLogged: boolean;

  roleSelected = false;
  location: LocationModel = new LocationModel();
  isCustomer = false;
  isVendor = false;
  isCompany = false;

  floader: boolean;
  gloader: boolean;
  tloader: boolean;

  constructor(
    public modal: SuiModal<void>,
    private zone: NgZone,
    private helperService: HelperService,
    public registerService: RegisterService,
    public authLoginService: AuthenticationLoginService,
    private authEventService: AuthenticationEventService,
    private locationService: LocationService,
    private apiLoader: NgMapAsyncApiLoader
  ) {

    this.mode = 'date';
    this.authLoginService.signOut();

    this.isLogged = false;
    this.error = false;
    this.location = this.locationService.getCurrentLocation();
    this.initRoles();
  }

  isLoading(): boolean {
    return this.floader || this.gloader || this.tloader;
  }

  handleErrorLogin() {
    this.zone.run(() => this.error = true);
  }

  handleResponse(resp: any): void {
    switch (resp.status) {
      case 204: {
        this.error = false;
        this.zone.run(() => this.isLogged = !this.isLogged);
        break;
      }
      case 200: {
        this.modal.deny(null);
        this.authLoginService.saveJwt(resp.headers.get('token'));
        this.error = false;
        this.authEventService.signIn();
        this.zone.run(() => this.helperService.redirectAfterAuthentication());
        break;
      }
      default: {
        this.handleErrorLogin();
        break;
      }
    }
  }

  loginWithGoogle() {
    this.gloader = true;
    this.authLoginService.loginWithGoogle()
      .then(resp => {
        this.gloader = false;
        this.handleResponse(resp);
      })
      .catch(err => {
        this.gloader = false;
        this.handleErrorLogin();
      });
  }

  loginWithFacebook() {
    this.floader = true;
    this.authLoginService.loginWithFacebook()
      .then(resp => {
        this.floader = false;
        this.handleResponse(resp);
      })
      .catch(err => {
        this.floader = false;
        this.handleErrorLogin();
      });
  }

  loginWithTwitter() {
    this.tloader = true;
    this.authLoginService.loginWithTwitter()
      .then(resp => {
        this.tloader = false;
        this.handleResponse(resp);
      })
      .catch(err => {
        this.tloader = false;
        this.handleErrorLogin();
      });
  }

  clearRoles() {
    for (let key in this.roles) {
      this.roles[key] = false;
    }
  }

  initRoles() {
    this.roles['customer'] = false;
    this.roles['vendor'] = false;
    this.roles['company'] = false;
  }

  ngOnInit() {
    
    this.apiLoader.load();
    this.locationService.getGoogle().then((g) => {
      this.locationService.getLocDetails(this.location.Latitude, this.location.Longitude).subscribe(
        result => {
          this.location.Adress=(result.address_components[1].short_name+','+result.address_components[0].short_name)
          this.location.City=result.address_components[3].short_name;
        }
      );
    });
    
    this.authLoginService.authState.subscribe(user => {
      if (user) {
        this.currentUser = user;
        
      } else {
        this.currentUser = null;
      }
    });
  }

  ngOnDestroy() {
    this.error = false;
    this.isLogged = false;
    this.isCompany = false;
    this.isVendor = false;
    this.isCustomer = false;
    this.roleSelected = false;
    this.social = undefined;
    this.clearRoles();
  }

  selectRole(role: string) {
    
    this.clearRoles();
    this.roles[role] = true;
  }
}