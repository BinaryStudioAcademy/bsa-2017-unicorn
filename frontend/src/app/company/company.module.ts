import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'
import {SuiModule} from 'ng2-semantic-ui';
import { BookModule } from '../book/book.module';

import { CompanyComponent } from './company-component/company.component';
import { CompanyDetailsComponent } from './company-details/company-details.component';
import { CompanyRoutingModule } from './company-routing.module';
import { GeneralInformationComponent } from './company-details/general-information/general-information.component';
import { ReviewsComponent } from './company-details/reviews/reviews.component';
import { VendorsComponent } from './company-details/vendors/vendors.component';
import { ContactsComponent } from './company-details/contacts/contacts.component';
import { Company } from "../models/company.model";
import { Review } from "../models/review.model";
import { Vendor } from "../models/vendor";
import { environment } from "../../environments/environment";
import { AgmCoreModule } from "@agm/core";

@NgModule({
  imports: [
    CommonModule,
    CompanyRoutingModule,
    FormsModule,
    BrowserModule,
    SuiModule,
    BookModule,
    AgmCoreModule.forRoot({
      apiKey: environment.googleMapsKey
    }),   
  ],
  declarations: [
    CompanyComponent,
    CompanyDetailsComponent,
    GeneralInformationComponent,
    ReviewsComponent,
    VendorsComponent,
    ContactsComponent
  ]
})
export class CompanyModule { }
