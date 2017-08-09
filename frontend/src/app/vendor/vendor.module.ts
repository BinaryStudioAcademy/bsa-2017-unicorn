import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VendorComponent } from './vendor-component/vendor.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';
import { VendorProfileInfoComponent } from './vendor-profile-info/vendor-profile-info.component';
import { VendorProfileContactsComponent } from './vendor-profile-contacts/vendor-profile-contacts.component';
import { VendorProfileReviewsComponent } from './vendor-profile-reviews/vendor-profile-reviews.component';
import { VendorProfilePortfolioComponent } from './vendor-profile-portfolio/vendor-profile-portfolio.component';

import { VendorRoutingModule } from './vendor-routing.module';
import { VendorsComponent } from './vendors/vendors.component';

@NgModule({
  imports: [
    CommonModule,
    VendorRoutingModule
  ],
  declarations: [
    VendorComponent,
    VendorDetailsComponent,
    VendorProfileInfoComponent,
    VendorProfileContactsComponent,
    VendorProfileReviewsComponent,
    VendorProfilePortfolioComponent,
    VendorsComponent
  ]
})
export class VendorModule { }
