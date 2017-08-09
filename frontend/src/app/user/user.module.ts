import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserComponent } from './user-component/user.component';
import { UserDetailsComponent } from './user-details/user-details.component';

import { UserRoutingModule } from './user-routing.module';
import {SuiModule} from 'ng2-semantic-ui';

@NgModule({
  imports: [
    SuiModule,
    CommonModule,
    UserRoutingModule
  ],
  declarations: [
    UserComponent,
    UserDetailsComponent
  ]
})
export class UserModule { }
