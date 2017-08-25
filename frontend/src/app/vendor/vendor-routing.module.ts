import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { VendorsComponent } from './vendors/vendors.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';
import { VendorProfileInfoComponent } from './vendor-details/vendor-profile-info/vendor-profile-info.component';
import { VendorProfileContactsComponent } from './vendor-details/vendor-profile-contacts/vendor-profile-contacts.component';
import { VendorProfileReviewsComponent } from './vendor-details/vendor-profile-reviews/vendor-profile-reviews.component';
import { VendorProfilePortfolioComponent } from './vendor-details/vendor-profile-portfolio/vendor-profile-portfolio.component';
import { VendorEditComponent } from "./vendor-edit/vendor-edit.component";

import { AuthGuard } from '../guards/auth.guard';

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
            component: VendorDetailsComponent
          },
          {
            path: ':id/edit',
            component: VendorEditComponent,
            canActivate: [AuthGuard]
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ],
  providers: [AuthGuard]
})
export class VendorRoutingModule { }
