import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CompanyComponent } from './company-component/company.component';
import { CompanyDetailsComponent } from './company-details/company-details.component';
import { CompanyEditComponent } from "./company-edit/company-edit.component";

import { AuthGuard } from '../guards/auth.guard';

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
          },
          {
            path: ':id/edit',
            component: CompanyEditComponent,
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
export class CompanyRoutingModule { }
