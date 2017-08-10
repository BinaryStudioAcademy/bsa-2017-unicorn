import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { RegisterComponent } from './register-component/register.component';
import { RegisterRoutingModule } from './register-routing.module';

import { SuiModule } from 'ng2-semantic-ui';
import { Angular2SocialLoginModule } from 'angular2-social-login';
import { AuthService } from 'angular2-social-login';

import { providers } from './social-providers';

@NgModule({
  imports: [
    FormsModule,
    SuiModule,
    CommonModule,
    RegisterRoutingModule
  ],
  declarations: [
    RegisterComponent
  ],
  providers: [AuthService]
})
export class RegisterModule { }

Angular2SocialLoginModule.loadProvidersScripts(providers);
