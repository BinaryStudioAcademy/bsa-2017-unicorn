import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CompanyComponent } from './company-component/company.component';
import { CompanyDetailsComponent } from './company-details/company-details.component';
import { CompanyRoutingModule } from './company-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CompanyRoutingModule
  ],
  declarations: [
    CompanyComponent,
    CompanyDetailsComponent
  ]
})
export class CompanyModule { }
