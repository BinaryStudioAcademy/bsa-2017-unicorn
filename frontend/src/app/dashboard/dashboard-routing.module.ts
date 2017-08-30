import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AuthGuard } from '../guards/auth.guard';
import { DashboardGuard } from '../guards/dashboard.guard';

import { DashboardComponent } from './dashboard-component/dashboard.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'dashboard',
        component: DashboardComponent,
        canActivate: [DashboardGuard, AuthGuard]
      }
    ]),
    RouterModule.forChild([
      // TODO: add routes for dashboard
    ])
  ],
  exports: [
    RouterModule
  ],
  providers: [
    DashboardGuard
  ]
})
export class DashboardRoutingModule { }
