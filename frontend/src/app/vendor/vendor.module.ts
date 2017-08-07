import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { VendorComponent } from './vendor-component/vendor.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';

import { VendorRoutingModule } from './vendor-routing.module';

@NgModule({
  imports: [
    CommonModule,
    VendorRoutingModule
  ],
  declarations: [
    VendorComponent,
    VendorDetailsComponent
  ]
})
export class VendorModule { }
