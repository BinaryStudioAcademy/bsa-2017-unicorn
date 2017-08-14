import { Component, OnInit, OnDestroy, Input, ViewChild, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';

import { RegisterService } from '../../services/register.service';

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
  import 'rxjs/add/operator/map';

import { RegisterInfo } from '../models/register-info';

export interface IContext { }

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService]
})
export class RegisterComponent implements OnInit, OnDestroy {

  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<IContext, string, string>
  private activeModal: SuiActiveModal<IContext, {}, string>;

  private authState: Observable<firebase.User>
  private currentUser: firebase.User = null;

  @Input() enabled: boolean;

  mode: string;
  modalSize: string;
  error: boolean = false;

  public roles: {[role: string]: boolean} = {};

  social: any;
  sub: any;

  isLogged: boolean = false;

  roleSelected = false;

  isCustomer = false;
  isVendor = false;
  isCompany = false;

  constructor(
    public modalService: SuiModalService,
    public router: Router,
    public registerService: RegisterService,
    public afAuth: AngularFireAuth,
    public ref: ChangeDetectorRef) {

  }

  handleErrorLogin() {
    this.error = true;
  }

  saveToken(token: string) {
    console.log('token: ' + token);
    localStorage.setItem('token', token);
  }

  handleResponse(resp: any): void {
    console.log('resp: ' + resp);
    console.log('status: ' + resp.status);
    switch (resp.status) {
      case 204: this.isLogged = true; this.error = false; break;
      case 200: this.saveToken(resp.headers.get('token')); console.log(resp.headers); this.error = false; break;
      default: this.handleErrorLogin(); break;
    }
  }

  checkRegistration(provider: string, uid: string) {
    this.registerService
      .checkAuthorized(provider, uid)
      .then(resp => {this.handleResponse(resp)})
      .catch(err => {
        console.log('error: ' + err.status);
        this.handleErrorLogin();
      });
  }

  loginWithGoogle() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GoogleAuthProvider())
      .then(x => {
         console.log(x);
        // let uid = x.user.providerData[0].uid;
        // let provider = x.additionalUserInfo.providerId;
        // this.checkRegistration(provider, uid);
      })
      .catch(err => {
        this.handleErrorLogin();
      });
  }

  loginWithFacebook() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.FacebookAuthProvider())
      .then(x => {
        console.log(x);
      })
      .catch(err => {
        this.handleErrorLogin();
      });
  }

  loginWithTwitter() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.TwitterAuthProvider())
      .then(x => {
        console.log(x);
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
    this.mode = 'date';
    //this.modalSize = 'tiny';

    this.authState = this.afAuth.authState;
    this.authState.subscribe(user => {
      if (user) {
        this.currentUser = user;
        let uid = user.providerData[0].providerId;
        let provider = user.uid;
        this.checkRegistration(provider, uid);
      } 
    });

    this.initRoles();
  }

  ngOnDestroy() {
  }

  private closeModal() {
    this.activeModal.deny(''); 
  }

  public openModal() {
    const config = new TemplateModalConfig<IContext, string, string>(this.modalTemplate);
    let size = 'tiny';
    config.closeResult = "closed!";
    config.context = {};
    config.size = ModalSize.Small;
    config.isInverted = true;

    this.activeModal = this.modalService
      .open(config)
      .onApprove(result => { /* approve callback */ })
      .onDeny(result => {
        this.isLogged = false;
        this.isCompany = false;
        this.isVendor = false;
        this.isCustomer = false;
        this.roleSelected = false;
        this.social = undefined;
        this.clearRoles();
      });

    this.afAuth.auth.signOut();
    this.isLogged = false;
  }

  selectRole(role: string) {
    // switch (role) {
    //   case 'customer': this.isCustomer = true; break;
    //   case 'vendor': this.isVendor = true; break;
    //   case 'company': this.isCompany = true; break;
    // }
    // this.roleSelected = true;
    this.clearRoles();  
    this.roles[role] = true;
  }
}