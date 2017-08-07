import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { DashboardComponent } from './dashboard-component/dashboard.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'dashboard',
        component: DashboardComponent,
      }
    ]),
    RouterModule.forChild([
      // TODO: add routes for dashboard
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class DashboardRoutingModule { }
