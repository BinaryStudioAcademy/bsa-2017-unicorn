import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpModule } from '@angular/http';

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
import { AgmCoreModule } from "@agm/core";
import { environment } from "../../environments/environment";
import {ImageCropperComponent} from 'ng2-img-cropper';

@NgModule({
    imports: [
        AgmCoreModule.forRoot({
            apiKey: environment.googleMapsKey
        }),
        SuiModule,
        CommonModule,
        UserRoutingModule,
        HttpModule

    ],
    declarations: [
        UserComponent,
        UserDetailsComponent,
        UserProfileComponent,
        UserHistoryComponent,
        UserTasksComponent,
        UserMessagesComponent,
        ImageCropperComponent
    ],
    providers: [
        DataService,
        UserService
    ]
})
export class UserModule { }
