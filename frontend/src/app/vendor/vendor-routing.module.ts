import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { VendorsComponent } from './vendors/vendors.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';
import { VendorProfileInfoComponent } from './vendor-profile-info/vendor-profile-info.component';
import { VendorProfileContactsComponent } from './vendor-profile-contacts/vendor-profile-contacts.component';
import { VendorProfileReviewsComponent } from './vendor-profile-reviews/vendor-profile-reviews.component';
import { VendorProfilePortfolioComponent } from './vendor-profile-portfolio/vendor-profile-portfolio.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'vendors',
        component: VendorsComponent,
      }
    ]),
    RouterModule.forChild([
      {
        path: 'vendor',
        children: [
          {
            path: '',
            redirectTo: '/vendors',
            pathMatch: 'full'
          },
          {
            path: ':id',
            component: VendorDetailsComponent,
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class VendorRoutingModule { }
