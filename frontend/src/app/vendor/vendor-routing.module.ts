import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { VendorComponent } from './vendor-component/vendor.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: "vendor", component: VendorComponent },
      { path: "vendor/:id", component: VendorDetailsComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class VendorRoutingModule { }
