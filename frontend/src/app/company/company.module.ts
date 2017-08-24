import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'
import {SuiModule} from 'ng2-semantic-ui';
import { BookModule } from '../book/book.module';
import { SignBlockModule } from '../sign-block/sign-block.module';

import { CompanyComponent } from './company-component/company.component';
import { CompanyDetailsComponent } from './company-details/company-details.component';
import { CompanyRoutingModule } from './company-routing.module';
import { GeneralInformationComponent } from './company-details/general-information/general-information.component';
import { ReviewsComponent } from './company-details/reviews/reviews.component';
import { VendorsComponent } from './company-details/vendors/vendors.component';
import { ContactsComponent } from './company-details/contacts/contacts.component';
import { Review } from "../models/review.model";
import { environment } from "../../environments/environment";
import { CompanyEditComponent } from './company-edit/company-edit.component';
import { DataService } from "../services/data.service";
import { PhotoService } from '../services/photo.service';
import { NguiMapModule } from "@ngui/map";
import { MapModule } from "../map/map.module";
import { CompanyMainInformationComponent } from './company-edit/company-main-information/company-main-information.component';
import { CompanyMessagesComponent } from './company-edit/company-messages/company-messages.component';
import { CompanyOrdersComponent } from './company-edit/company-orders/company-orders.component';
import { CompanyService } from "../services/company-services/company.service";
import { CompanyVendorsComponent } from './company-edit/company-vendors/company-vendors.component';
import { CompanyWorksComponent } from './company-edit/company-works/company-works.component';
import { PipeModule } from "../pipe/pipe.module";

@NgModule({
  imports: [    
    CommonModule,
    CompanyRoutingModule,
    FormsModule,
    BrowserModule,
    SuiModule,
    BookModule,
    SignBlockModule,
    MapModule,
    PipeModule
  ],
  declarations: [
    CompanyComponent,
    CompanyDetailsComponent,
    GeneralInformationComponent,
    ReviewsComponent,
    VendorsComponent,
    ContactsComponent,
    CompanyEditComponent,
    CompanyMainInformationComponent,
    CompanyMessagesComponent,
    CompanyOrdersComponent,
    CompanyVendorsComponent,
    CompanyWorksComponent
    
  ],
  providers: [
    DataService,
    PhotoService,
    CompanyService
  ]
})
export class CompanyModule { }
