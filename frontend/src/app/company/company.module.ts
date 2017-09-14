import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule } from '@angular/forms';
import {SuiModule} from 'ng2-semantic-ui';
import { NgxChartsModule } from '@swimlane/ngx-charts';
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
import { OfferService } from '../services/offer.service';
import { ChartService } from '../services/charts/chart.service';
import { MapModule } from "../map/map.module";
import { CompanyMainInformationComponent } from './company-edit/company-main-information/company-main-information.component';
import { CompanyMessagesComponent } from './company-edit/company-messages/company-messages.component';
import { CompanyOrdersComponent } from './company-edit/company-orders/company-orders.component';
import { CompanyService } from "../services/company-services/company.service";
import { CompanyVendorsComponent } from './company-edit/company-vendors/company-vendors.component';
import { CompanyWorksComponent } from './company-edit/company-works/company-works.component';
import { PipeModule } from "../pipe/pipe.module";
import { ClickOutsideModule } from 'ng-click-outside';
import { ImageCropperComponent, ImageCropperModule } from 'ng2-img-cropper/index';
import { CompanyContactsComponent } from './company-edit/company-contacts/company-contacts.component';
import { ChatModule } from "../chat/chat.module";
import { PortfolioComponent } from './company-details/portfolio/portfolio.component';
import { CompanyPortfolioComponent } from './company-edit/company-portfolio/company-portfolio.component';
import { NguiMapModule } from "@ngui/map/dist";
import { OwnCalendarModule } from "../calendar/calendar.module";
import { CompanyCalendarComponent } from './company-edit/company-calendar/company-calendar.component';
import { CompanyChartsComponent } from './company-edit/company-charts/company-charts.component';


@NgModule({
  imports: [
    CommonModule,
    CompanyRoutingModule,
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    SuiModule,
    NgxChartsModule,
    BookModule,
    SignBlockModule,
    MapModule,
    PipeModule,
    ClickOutsideModule,
    ImageCropperModule,
    ChatModule,
    OwnCalendarModule,
    NguiMapModule.forRoot({
      apiUrl: 'https://maps.google.com/maps/api/js?key=' + environment.googleMapsKey +
      '&libraries=visualization,places,drawing'
    })
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
    CompanyWorksComponent,
    CompanyContactsComponent,
    PortfolioComponent,
    CompanyPortfolioComponent,
    CompanyCalendarComponent,
    CompanyChartsComponent
    
  ],
  providers: [
    DataService,
    PhotoService,
    CompanyService,
    OfferService,
    ChartService
  ]
})
export class CompanyModule { }
