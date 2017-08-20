import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { UserComponent } from './user-component/user.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserMainComponent} from './user-main/user-main.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'users',
        component: UserComponent,
      }
    ]),
    RouterModule.forChild([
      {
        path: 'user',
        children: [
          {
            path: '',
            redirectTo: '/users',
            pathMatch: 'full'
          },
          {
            path: ':id/edit',
            component: UserDetailsComponent,
          },
          {
            path: ':id',
            component: UserMainComponent,
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class UserRoutingModule { }
