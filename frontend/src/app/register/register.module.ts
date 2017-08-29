import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Location } from '@angular/common';

import { RegisterComponent } from './register-component/register.component';
import { RegisterRoutingModule } from './register-routing.module';
import { environment } from "../../environments/environment";

import { SuiModule } from 'ng2-semantic-ui';

import { RegisterUserComponent } from './register-user/register-user.component';
import { RegisterVendorComponent } from './register-vendor/register-vendor.component';
import { RegisterCompanyComponent } from './register-company/register-company.component';
import { NguiMapModule } from "@ngui/map";

@NgModule({
  imports: [
    NguiMapModule.forRoot({
      apiUrl: 'https://maps.google.com/maps/api/js?key=' + environment.googleMapsKey +
      '&libraries=visualization,places,drawing'
    }),
    FormsModule,
    SuiModule,
    CommonModule,
    RegisterRoutingModule
  ],
  declarations: [
    RegisterComponent,
    RegisterUserComponent,
    RegisterVendorComponent,
    RegisterCompanyComponent
  ],
  exports: [RegisterComponent],
  providers: []
})
export class RegisterModule { }