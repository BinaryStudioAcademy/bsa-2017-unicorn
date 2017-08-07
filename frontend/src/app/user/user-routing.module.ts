import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { UserComponent } from './user-component/user.component';
import { UserDetailsComponent } from './user-details/user-details.component';

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
            path: ':id',
            component: UserDetailsComponent,
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
