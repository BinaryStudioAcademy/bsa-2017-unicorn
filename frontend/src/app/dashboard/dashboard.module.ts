import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DashboardComponent } from './dashboard-component/dashboard.component';
import { DashboardPendingsComponent } from './dashboard-component/dashboard-pendings/dashboard-pendings.component';
import { DashboardProgressComponent } from './dashboard-component/dashboard-progress/dashboard-progress.component';
import { DashboardFinishedComponent } from './dashboard-component/dashboard-finished/dashboard-finished.component';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SuiModule } from 'ng2-semantic-ui';
import {ToastModule, ToastsManager, ToastOptions} from 'ng2-toastr/ng2-toastr';

import { DataService } from '../services/data.service';
import { DashboardService } from '../services/dashboard/dashboard.service';
import { TokenHelperService } from '../services/helper/tokenhelper.service';
import { DashMessagingService } from '../services/dashboard/dash-messaging.service';


@NgModule({
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SuiModule,
    FormsModule,
    ToastModule.forRoot()
  ],
  declarations: [
    DashboardComponent,
    DashboardPendingsComponent,
    DashboardProgressComponent,
    DashboardFinishedComponent
  ],
  providers: [
    DashboardService,
    DataService,
    DashMessagingService
  ]
})
export class DashboardModule { }
