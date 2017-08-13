import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserComponent } from './user-component/user.component';
import { UserDetailsComponent } from './user-details/user-details.component';

import { UserRoutingModule } from './user-routing.module';
import {SuiModule} from 'ng2-semantic-ui';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { UserHistoryComponent } from './user-history/user-history.component';
import { UserTasksComponent } from './user-tasks/user-tasks.component';
import { UserMessagesComponent } from './user-messages/user-messages.component';

import { UserService } from '../services/user.service';
import { DataService } from "../services/data.service";

@NgModule({
  imports: [
    SuiModule,
    CommonModule,
    UserRoutingModule
  ],
  declarations: [
    UserComponent,
    UserDetailsComponent,
    UserProfileComponent,
    UserHistoryComponent,
    UserTasksComponent,
    UserMessagesComponent
  ],
  providers: [
    DataService,
    UserService
  ]
})
export class UserModule { }
