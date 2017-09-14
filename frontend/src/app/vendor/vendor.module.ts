import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { SuiModule } from 'ng2-semantic-ui';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MdlModule } from '@angular-mdl/core';
import { environment } from '../../environments/environment';
import { ClickOutsideModule } from 'ng-click-outside';

import { VendorRoutingModule } from './vendor-routing.module';
import { SignBlockModule } from '../sign-block/sign-block.module';
import { BookModule } from '../book/book.module';
import { MapModule } from '../map/map.module';
import { ImageCropperComponent, ImageCropperModule } from 'ng2-img-cropper/index';
import { PipeModule } from "../pipe/pipe.module";

import { VendorsComponent } from './vendors/vendors.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';
import { VendorProfileInfoComponent } from './vendor-details/vendor-profile-info/vendor-profile-info.component';
import { VendorProfileContactsComponent } from './vendor-details/vendor-profile-contacts/vendor-profile-contacts.component';
import { VendorProfileReviewsComponent } from './vendor-details/vendor-profile-reviews/vendor-profile-reviews.component';
import { VendorProfilePortfolioComponent } from './vendor-details/vendor-profile-portfolio/vendor-profile-portfolio.component';
import { VendorEditPortfolioComponent } from "./vendor-edit/vendor-edit-portfolio/vendor-edit-portfolio.component";
import { VendorEditWorksComponent } from './vendor-edit/vendor-edit-works/vendor-edit-works.component';
import { VendorEditComponent } from './vendor-edit/vendor-edit.component';
import { VendorEditInfoComponent } from './vendor-edit/vendor-edit-info/vendor-edit-info.component';
import { VendorEditOrdersComponent } from './vendor-edit/vendor-edit-orders/vendor-edit-orders.component';

import { PhotoService } from '../services/photo.service';
import { ChartService } from '../services/charts/chart.service';
import { VendorService } from '../services/vendor.service';
import { ReviewService } from '../services/review.service';
import { DataService } from "../services/data.service";
import { LocationService } from "../services/location.service";
import { DashboardService } from '../services/dashboard/dashboard.service';
import { PerformerService } from '../services/performer.service';

import { CategoryService } from "../services/category.service";
import { WorkService } from "../services/work.service";
import { VendorEditContactsComponent } from './vendor-edit/vendor-edit-contacts/vendor-edit-contacts.component';
import { ContactService } from "../services/contact.service";
import { NguiMapModule } from "@ngui/map/dist";
import { ChatModule } from "../chat/chat.module";
import { VendorEditMessagesComponent } from './vendor-edit/vendor-edit-messages/vendor-edit-messages.component';
import { OwnCalendarModule } from "../calendar/calendar.module";
import { VendorEditChartsComponent } from './vendor-edit/vendor-edit-charts/vendor-edit-charts.component';
import { VendorEditCalendarComponent } from './vendor-edit/vendor-edit-calendar/vendor-edit-calendar.component';


@NgModule({
  imports: [
    NguiMapModule.forRoot({
      apiUrl: 'https://maps.google.com/maps/api/js?key=' + environment.googleMapsKey +
      '&libraries=visualization,places,drawing'
    }),
    VendorRoutingModule,
    FormsModule,
    BrowserAnimationsModule,
    NgxChartsModule,
    SuiModule,
    BookModule,
    MapModule,
    SignBlockModule,
    CommonModule,
    ImageCropperModule,
    PipeModule,
    ChatModule,
    ClickOutsideModule,
    OwnCalendarModule,
    NguiMapModule.forRoot({
      apiUrl: 'https://maps.google.com/maps/api/js?key=' + environment.googleMapsKey +
      '&libraries=visualization,places,drawing'
    }),
    MdlModule
  ],
  declarations: [
    VendorDetailsComponent,
    VendorProfileInfoComponent,
    VendorProfileContactsComponent,
    VendorProfileReviewsComponent,
    VendorProfilePortfolioComponent,
    VendorsComponent,
    VendorEditComponent,
    VendorEditInfoComponent,
    VendorEditOrdersComponent,
    VendorEditPortfolioComponent,
    VendorEditWorksComponent,
    VendorEditContactsComponent,
    VendorEditMessagesComponent,
    VendorEditChartsComponent,
    VendorEditCalendarComponent
  ],
  providers: [
    DataService,
    ChartService,
    VendorService,
    LocationService,
    ReviewService,
    PhotoService,
    CategoryService,
    WorkService,
    ContactService,
    DashboardService,
    PerformerService
  ]
})
export class VendorModule { }
