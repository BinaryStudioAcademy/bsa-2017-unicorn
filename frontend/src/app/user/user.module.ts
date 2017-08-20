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
import { AgmCoreModule } from "@agm/core";
import { environment } from "../../environments/environment";
import { ImageCropperComponent} from 'ng2-img-cropper';
import { UserMainComponent} from './user-main/user-main.component';
import { UserMainInfoComponent } from './user-main/user-main-info/user-main-info.component';
import { UserMainReviewsComponent } from './user-main/user-main-reviews/user-main-reviews.component';

@NgModule({
    imports: [
        AgmCoreModule.forRoot({
            apiKey: environment.googleMapsKey
        }),
        SuiModule,
        CommonModule,
        UserRoutingModule,
        HttpModule,
        FormsModule

    ],
    declarations: [
        UserComponent,
        UserDetailsComponent,
        UserProfileComponent,
        UserHistoryComponent,
        UserTasksComponent,
        UserMessagesComponent,
        ImageCropperComponent,
        UserMainComponent,
        UserMainInfoComponent,
        UserMainReviewsComponent
    ],
    providers: [
        DataService,
        UserService
    ]
})
export class UserModule { }
