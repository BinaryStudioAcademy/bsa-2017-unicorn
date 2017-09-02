import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { DbComponent } from './db/db.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: "db", component: DbComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class DbRoutingModule { }
