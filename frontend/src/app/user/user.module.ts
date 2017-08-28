import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { UserComponent } from './user-component/user.component';
import { UserDetailsComponent } from './user-details/user-details.component';

import { UserRoutingModule } from './user-routing.module';
import { SuiModule } from 'ng2-semantic-ui';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserHistoryComponent } from './user-history/user-history.component';
import { UserTasksComponent } from './user-tasks/user-tasks.component';
import { UserMessagesComponent } from './user-messages/user-messages.component';

import { PhotoService, Ng2ImgurUploader } from '../services/photo.service';
import { UserService } from '../services/user.service';
import { DataService } from "../services/data.service";
import { CustomerbookService } from '../services/customerbook.service';
import { AgmCoreModule } from "@agm/core";
import { environment } from "../../environments/environment";
import { UserMainComponent } from './user-main/user-main.component';
import { UserMainInfoComponent } from './user-main/user-main-info/user-main-info.component';
import { UserMainReviewsComponent } from './user-main/user-main-reviews/user-main-reviews.component';
import { SharedModule } from "../shared/shared.module";
import {BrowserModule} from '@angular/platform-browser';
import {ToastModule, ToastsManager, ToastOptions} from 'ng2-toastr/ng2-toastr';



@NgModule({
    imports: [
        AgmCoreModule.forRoot({
            apiKey: environment.googleMapsKey
        }),
        SuiModule,
        CommonModule,
        UserRoutingModule,
        HttpModule,
        FormsModule,
        FormsModule,
        SharedModule,
        BrowserModule, 
        ToastModule.forRoot()

    ],
    declarations: [
        UserComponent,
        UserDetailsComponent,
        UserProfileComponent,
        UserHistoryComponent,
        UserTasksComponent,
        UserMessagesComponent,
        UserMainComponent,
        UserMainInfoComponent,
        UserMainReviewsComponent
    ],
    providers: [
        DataService,
        UserService,
        CustomerbookService
    ]
})
export class UserModule { }
