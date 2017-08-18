import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { Location } from '@angular/common';

import { AuthenticationLoginService } from '../../services/auth/authenticationlogin.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';

import * as firebase from 'firebase/app';

import { HelperService } from '../../services/helper/helper.service';

import { RegisterService } from '../../services/register.service';

import { ComponentModalConfig, ModalSize, SuiModal } from 'ng2-semantic-ui';

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
  providers: [RegisterService]
})
export class RegisterComponent implements OnInit, OnDestroy {
  private currentUser: firebase.User = null;

  mode: string;
  error: boolean = false;

  public roles: { [role: string]: boolean } = {};

  social: any;

  isLogged: boolean;

  roleSelected = false;

  isCustomer = false;
  isVendor = false;
  isCompany = false;

  constructor(
    public modal: SuiModal<void>,
    private zone: NgZone,
    private helperService: HelperService,
    public registerService: RegisterService,
    public authLoginService: AuthenticationLoginService,
    private authEventService: AuthenticationEventService) {

    this.mode = 'date';
    this.authLoginService.signOut();

    this.isLogged = false;
    this.error = false;

    this.initRoles();
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
        this.modal.deny(undefined);
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
    this.authLoginService.loginWithGoogle()
      .then(resp => {
        this.handleResponse(resp);
      })
      .catch(err => {
        this.handleErrorLogin();
      });
  }

  loginWithFacebook() {
    this.authLoginService.loginWithFacebook()
      .then(resp => {
        this.handleResponse(resp);
      })
      .catch(err => {
        this.handleErrorLogin();
      });
  }

  loginWithTwitter() {
    this.authLoginService.loginWithTwitter()
      .then(resp => {
        this.handleResponse(resp);
      })
      .catch(err => {
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