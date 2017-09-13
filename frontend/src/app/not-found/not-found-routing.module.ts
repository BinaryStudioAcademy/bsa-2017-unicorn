import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import {NotFoundComponent} from './not-found/not-found.component'
@NgModule({
  imports: [
    RouterModule.forChild([
      { path: "not-found", component: NotFoundComponent },
      
     { 
       path: '**',
       component: NotFoundComponent
     }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class NotFoundRoutingModule { }
