import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { UserComponent } from './user-component/user.component';
import { UserDetailsComponent } from './user-details/user-details.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: "users", component: UserComponent },
      { path: "user/:id", component: UserDetailsComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class UserRoutingModule { }
