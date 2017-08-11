import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Location } from '@angular/common';

import { RegisterComponent } from './register-component/register.component';
import { RegisterRoutingModule } from './register-routing.module';

import { SuiModule } from 'ng2-semantic-ui';
import { Angular2SocialLoginModule } from 'angular2-social-login';
import { AuthService } from 'angular2-social-login';

import { providers } from './social-providers';
import { RegisterUserComponent } from './register-user/register-user.component';
import { RegisterVendorComponent } from './register-vendor/register-vendor.component';
import { RegisterCompanyComponent } from './register-company/register-company.component';

@NgModule({
  imports: [
    FormsModule,
    SuiModule,
    CommonModule,
    RegisterRoutingModule,
    Angular2SocialLoginModule
  ],
  declarations: [
    RegisterComponent,
    RegisterUserComponent,
    RegisterVendorComponent,
    RegisterCompanyComponent
  ],
  providers: [AuthService]
})
export class RegisterModule { }

Angular2SocialLoginModule.loadProvidersScripts(providers);
