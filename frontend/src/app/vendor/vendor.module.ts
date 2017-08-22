import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { SuiModule } from 'ng2-semantic-ui';
import { environment } from '../../environments/environment';
import { NguiMapModule } from "@ngui/map/dist";


import { SignBlockModule } from '../sign-block/sign-block.module';
import { BookModule } from '../book/book.module';
import { MapModule } from '../map/map.module';
import { SharedModule } from "../shared/shared.module";

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

import { VendorRoutingModule } from './vendor-routing.module';

import { EnumKeysPipe } from "../pipes/enum.pipe";

import { PhotoService } from '../services/photo.service';
import { VendorService } from '../services/vendor.service';
import { ReviewService } from '../services/review.service';
import { DataService } from "../services/data.service";
import { LocationService } from "../services/location.service";
import { CategoryService } from "../services/category.service";
import { WorkService } from "../services/work.service";

@NgModule({
  imports: [
    VendorRoutingModule,
    FormsModule,
    SuiModule,
    BookModule,
    MapModule,
    SignBlockModule,
    CommonModule,
    SharedModule
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
    EnumKeysPipe
  ],
  providers: [
    DataService,
    VendorService,
    LocationService,
    ReviewService,
    PhotoService,
    CategoryService,
    WorkService
  ]
})
export class VendorModule { }
