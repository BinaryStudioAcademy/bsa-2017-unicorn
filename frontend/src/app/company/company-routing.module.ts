import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CompanyComponent } from './company-component/company.component';
import { CompanyDetailsComponent } from './company-details/company-details.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: "company", component: CompanyComponent },
      { path: "company/:id", component: CompanyDetailsComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class CompanyRoutingModule { }
