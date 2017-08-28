import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { SuiModule } from 'ng2-semantic-ui';
import { environment } from '../../environments/environment';
import { NguiMapModule } from "@ngui/map/dist";
import { ClickOutsideModule } from 'ng-click-outside';

import { VendorRoutingModule } from './vendor-routing.module';
import { SignBlockModule } from '../sign-block/sign-block.module';
import { BookModule } from '../book/book.module';
import { MapModule } from '../map/map.module';
import { SharedModule } from "../shared/shared.module";
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
import { VendorService } from '../services/vendor.service';
import { ReviewService } from '../services/review.service';
import { DataService } from "../services/data.service";
import { LocationService } from "../services/location.service";
import { DashboardService } from '../services/dashboard/dashboard.service';

import { CategoryService } from "../services/category.service";
import { WorkService } from "../services/work.service";
import { VendorEditContactsComponent } from './vendor-edit/vendor-edit-contacts/vendor-edit-contacts.component';
import { ContactService } from "../services/contact.service";


@NgModule({
  imports: [
    VendorRoutingModule,
    FormsModule,
    SuiModule,
    BookModule,
    MapModule,
    SignBlockModule,
    CommonModule,
    SharedModule,
    ClickOutsideModule,    
    PipeModule
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
    VendorEditContactsComponent
  ],
  providers: [
    DataService,
    VendorService,
    LocationService,
    ReviewService,
    PhotoService,
    CategoryService,
    WorkService,
    ContactService,
    DashboardService
  ]
})
export class VendorModule { }
