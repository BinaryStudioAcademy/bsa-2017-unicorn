import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { IndexComponent } from './index-component/index.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: "index",
        component: IndexComponent
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class IndexRoutingModule { }
