import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { VendorComponent } from './vendor-component/vendor.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'vendors',
        component: VendorComponent,
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
