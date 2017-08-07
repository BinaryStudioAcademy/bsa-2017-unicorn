import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CompanyComponent } from './company-component/company.component';
import { CompanyDetailsComponent } from './company-details/company-details.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'companies',
        component: CompanyComponent,
      }
    ]),
    RouterModule.forChild([
      {
        path: 'company',
        children: [
          {
            path: '',
            redirectTo: '/companies',
            pathMatch: 'full'
          },
          {
            path: ':id',
            component: CompanyDetailsComponent,
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class CompanyRoutingModule { }
