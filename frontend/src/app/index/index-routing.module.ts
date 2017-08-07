import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { IndexComponent } from './index-component/index.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "index",
        component: IndexComponent
      },
      {
        path: '**',
        redirectTo: '/index',
        pathMatch: 'full'
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class IndexRoutingModule { }
