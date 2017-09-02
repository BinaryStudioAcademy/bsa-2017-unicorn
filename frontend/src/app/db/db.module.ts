import { NgModule } from '@angular/core';
import { DbComponent } from './db/db.component';
import { DbRoutingModule } from './db-routing.module';
import { DbcreationService } from '../services/helper/dbcreation.service';

@NgModule({
  imports: [
    DbRoutingModule
  ],
  declarations: [
    DbComponent
  ],
  providers: [
    DbcreationService
  ]
})
export class DbModule { }
