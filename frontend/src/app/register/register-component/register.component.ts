import { Component, OnInit, OnDestroy, Input, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { AuthService } from 'angular2-social-login';
import { RegisterService } from '../../services/register.service';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize} from 'ng2-semantic-ui';

import { RegisterInfo } from '../models/register-info';

export interface IContext {}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService]
})
export class RegisterComponent implements OnInit, OnDestroy {

  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<IContext, string, string>

  @Input() enabled: boolean;

  mode: string;
  modalSize: string;

  public user;
  sub: any;
  isLogged: boolean = false;
  roleSelected = false;

  isCustomer = false;
  isVendor = false;
  isCompany = false;

  // error: boolean = false;

  // phone: string;
  // birthday;
  // gender: string;
  // options = ['Male', 'Female'];

  constructor(
    public modalService: SuiModalService,
    public auth: AuthService,
    // public location: Location,
    // public router: Router,
    public registerService: RegisterService) { }

  ngOnInit() {
    this.mode = 'date';
    this.modalSize = 'tiny';
    //this.openModal();
  }

  ngOnDestroy() {
    //this.sub.unsubscribe();
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
          this.user = undefined;
        });
  }

  selectRole(role: string) {
    switch (role) {
      case 'customer': this.isCustomer = true; break;
      case 'vendor': this.isVendor = true; break;
      case 'company': this.isCompany = true; break;
    }
    this.roleSelected = true;
  }

  register(provider: string) {
    this.sub = this.auth.login(provider).subscribe(
      (data) => {
        console.log(data);
        this.user=data;
        this.isLogged = true;
        this.registerService.checkAuthorized(this.user.provider, this.user.uid);
      }
    )
  }

  logout() {
    this.auth.logout().subscribe(
      (data) => {
        console.log(data);
        this.user=null;
        this.isLogged = false;
      }
    )
  }

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
