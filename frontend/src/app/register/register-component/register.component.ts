import { Component, OnInit, OnDestroy, Input, ViewChild, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { AngularFireAuth } from 'angularfire2/auth';
import * as firebase from 'firebase/app';
import { Observable } from 'rxjs/Observable';
//import { AuthService } from 'angular2-social-login';

import { RegisterService } from '../../services/register.service';

import { SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize } from 'ng2-semantic-ui';

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

  private authState: Observable<firebase.User>
  private currentUser: firebase.User = null;

  @Input() enabled: boolean;

  mode: string;
  modalSize: string;

  social: any;
  sub: any;

  isLogged: boolean = false;

  roleSelected = false;

  isCustomer = false;
  isVendor = false;
  isCompany = false;

  constructor(
    public modalService: SuiModalService,

    //public auth: AuthService,
    // public location: Location,
    // public router: Router,
    public registerService: RegisterService,
    public afAuth: AngularFireAuth,
    public ref: ChangeDetectorRef) {
    
  }

  loginWithGoogle() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.GoogleAuthProvider())
      .then(x => {
        // now we can work with user object (take uid, email etc.)
        // TODO: call service and get token from server
        console.log(this.currentUser);
        //this.registerService
        //this.isLogged = true;
        //this.ref.detectChanges();
      })
      .catch(err => {
        // This prevent error in console, we can handle it there (user close popup error)
        alert(err);
      });
  }

  loginWithFacebook() {
    this.afAuth.auth.signInWithPopup(
      new firebase.auth.FacebookAuthProvider())
      .then(x => {
        console.log(x);
      })
      .catch(err => {
        alert(err);
      });
  }

  ngOnInit() {
    this.mode = 'date';
    this.modalSize = 'tiny';

    this.authState = this.afAuth.authState;
    this.authState.subscribe(user => {
      if (user) {
        this.isLogged = true;
        this.currentUser = user;
        //this.ref.detectChanges();
      } else {
        this.currentUser = null;
      }
    });
    
    //this.isLoggedOb = new Observable();
  }

  ngOnDestroy() {
  }

  public openModal() {
    const config = new TemplateModalConfig<IContext, string, string>(this.modalTemplate);
    let size = 'tiny';
    config.closeResult = "closed!";
    config.context = {};
    config.size = ModalSize.Small;
    config.isInverted = true;
    config.mustScroll = true;

    this.modalService
      .open(config)
      .onApprove(result => { /* approve callback */ })
      .onDeny(result => {
        this.isLogged = false;
        this.isCompany = false;
        this.isVendor = false;
        this.isCustomer = false;
        this.roleSelected = false;
        this.social = undefined;
      });

      this.afAuth.auth.signOut();
      this.isLogged = false;
  }

  selectRole(role: string) {
    switch (role) {
      case 'customer': this.isCustomer = true; break;
      case 'vendor': this.isVendor = true; break;
      case 'company': this.isCompany = true; break;
    }
    this.roleSelected = true;
  }

  // register(provider: string) {
  //   this.sub = this.auth.login(provider).subscribe(
  //     (data) => {
  //       console.log(data);
  //       this.user=data;
  //       this.isLogged = true;
  //       //this.registerService.checkAuthorized(this.user.provider, this.user.uid);
  //     }
  //   )
  // }

  // logout() {
  //   this.auth.logout().subscribe(
  //     (data) => {
  //       console.log(data);
  //       this.user=null;
  //       this.isLogged = false;
  //     }
  //   )
  // }

  // valid(): boolean {
  //   return this.birthday !== undefined && this.gender != undefined && this.phone != undefined;
  // }

  // aggregateInfo(): RegisterInfo{
  //   let info = new RegisterInfo();
  //   info.birthday = this.birthday;
  //   info.gender = this.gender;
  //   info.phone = this.phone;
  //   info.email = this.user.email;
  //   info.image = this.user.image;
  //   info.name = this.user.name;
  //   info.provider = this.user.provider;
  //   info.uid = this.user.uid;

  //   return info;
  // }

  // confirmRegister() {
  //   if (this.valid()) {
  //     this.error = false;
  //     console.log('valid');
  //     let regInfo = this.aggregateInfo();
  //     console.log(regInfo);
  //   } else {
  //     this.error = true;
  //   }
  // }

}
