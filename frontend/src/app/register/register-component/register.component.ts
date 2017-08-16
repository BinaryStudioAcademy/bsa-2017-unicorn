import { Component, OnInit, OnDestroy, Input, ViewChild, ChangeDetectorRef, forwardRef, Inject } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { AuthService } from '../../services/auth/auth.service';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';
import { MenuComponent } from '../../menu/menu.component';

import { RegisterService } from '../../services/register.service';

import {
  SuiModalService, TemplateModalConfig, SuiModal, ComponentModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal
} from 'ng2-semantic-ui';

export interface IConfirmModalContext {
  title: string;
  question: string;
}

export class ConfirmModal extends ComponentModalConfig<IConfirmModalContext, void, void> {
  constructor(title: string, question: string) {
    super(RegisterComponent, { title, question });
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

  @Input() enabled: boolean;

  mode: string;
  modalSize: string;
  error: boolean = false;

  public roles: { [role: string]: boolean } = {};

  social: any;

  isLogged: boolean = false;

  roleSelected = false;

  isCustomer = false;
  isVendor = false;
  isCompany = false;

  constructor(
    public modal: SuiModal<IConfirmModalContext, void, void>,
    public router: Router,
    public registerService: RegisterService,
    public authService: AuthService) {

    this.mode = 'date';
    this.authService.logout();

    this.authService.authState.subscribe(user => {
      if (user) {
        this.currentUser = user;
      } else {
        this.currentUser = null;
      }
    });

    this.isLogged = false;
    this.error = false;

    this.initRoles();
  }

  handleErrorLogin() {
    this.error = true;
  }

  handleResponse(resp: any): void {
    console.log('resp: ' + resp);
    console.log('status: ' + resp.status);
    switch (resp.status) {
      case 204: {
        this.isLogged = true;
        this.error = false;
        break;
      }
      case 200: {
        this.authService.saveJwt(resp.headers.get('token'));
        this.error = false;
        this.redirect();
        break;
      }
      default: {
        this.handleErrorLogin();
        break;
      }
    }
  }

  loginWithGoogle() {
    this.authService.loginWithGoogle()
      .then(resp => {
        this.handleResponse(resp);
      })
      .catch(err => {
        this.handleErrorLogin();
      });
  }

  loginWithFacebook() {
    this.authService.loginWithFacebook()
      .then(resp => {
        this.handleResponse(resp);
      })
      .catch(err => {
        this.handleErrorLogin();
      });
  }

  loginWithTwitter() {
    this.authService.loginWithTwitter()
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
    /*
    this.authService.afAuth.auth.onAuthStateChanged(user => {
      if (user) {
        this.currentUser = user;
      }
    });
    */
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

  private redirect() {
    this.modal.deny(undefined);
    // TODO initialize role and redirect to spec. page (/account, /vendor etc.)
    this.router.navigate(['index']);
  }

  selectRole(role: string) {
    this.clearRoles();
    this.roles[role] = true;
  }
}