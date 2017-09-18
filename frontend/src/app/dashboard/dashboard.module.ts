import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { DashboardComponent } from './dashboard-component/dashboard.component';
import { DashboardPendingsComponent } from './dashboard-component/dashboard-pendings/dashboard-pendings.component';
import { DashboardProgressComponent } from './dashboard-component/dashboard-progress/dashboard-progress.component';
import { DashboardFinishedComponent } from './dashboard-component/dashboard-finished/dashboard-finished.component';
import { DashboardOffersComponent } from './dashboard-component/dashboard-offers/dashboard-offers.component';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { SuiModule } from 'ng2-semantic-ui';
import {ToastModule, ToastsManager, ToastOptions} from 'ng2-toastr/ng2-toastr';

import { DataService } from '../services/data.service';
import { DashboardService } from '../services/dashboard/dashboard.service';
import { TokenHelperService } from '../services/helper/tokenhelper.service';
import { DashMessagingService } from '../services/dashboard/dash-messaging.service';
import { OfferService } from '../services/offer.service';
import { NguiMapModule } from "@ngui/map/dist";
import { environment } from "../../environments/environment";
import { MapModule } from '../map/map.module';
import { DashboardCompanyPendingsComponent } from './dashboard-component/dashboard-company-pendings/dashboard-company-pendings.component';
import { DashboardCompanyProgressComponent } from './dashboard-component/dashboard-company-progress/dashboard-company-progress.component';

@NgModule({
  imports: [
    NguiMapModule.forRoot({
      apiUrl: 'https://maps.google.com/maps/api/js?key=' + environment.googleMapsKey +
      '&libraries=visualization,places,drawing'
    }),
    CommonModule,
    DashboardRoutingModule,
    SuiModule,
    FormsModule,
    MapModule,
    ToastModule.forRoot()
  ],
  declarations: [
    DashboardComponent,
    DashboardPendingsComponent,
    DashboardProgressComponent,
    DashboardFinishedComponent,
    DashboardOffersComponent,
    DashboardCompanyPendingsComponent,
    DashboardCompanyProgressComponent
  ],
  providers: [
    DashboardService,
    DataService,
    DashMessagingService,
    TokenHelperService,
    OfferService,
    DatePipe
  ]
})
export class DashboardModule { }
