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
import { CompanyEditComponent } from './company-edit/company-edit.component';
import { DataService } from "../services/data.service";
import { CompanyService } from "../services/company.service";
import { NguiMapModule } from "@ngui/map";
import { MapComponent } from "../map/map.component";
import { CompanyMainInformationComponent } from './company-edit/company-main-information/company-main-information.component';
import { CompanyMessagesComponent } from './company-edit/company-messages/company-messages.component';
import { CompanyOrdersComponent } from './company-edit/company-orders/company-orders.component';

@NgModule({
  imports: [    
    NguiMapModule.forRoot({apiUrl: 'https://maps.google.com/maps/api/js?key='+ environment.googleMapsKey}),
    CommonModule,
    CompanyRoutingModule,
    FormsModule,
    BrowserModule,
    SuiModule,
    BookModule  
  ],
  declarations: [
    CompanyComponent,
    CompanyDetailsComponent,
    GeneralInformationComponent,
    ReviewsComponent,
    VendorsComponent,
    ContactsComponent,
    CompanyEditComponent,
    MapComponent,
    CompanyMainInformationComponent,
    CompanyMessagesComponent,
    CompanyOrdersComponent 
      ],
  providers: [
    DataService,
    CompanyService
  ]
})
export class CompanyModule { }
