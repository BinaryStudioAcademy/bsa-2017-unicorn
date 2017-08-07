import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IndexComponent } from './index-component/index.component';
import { IndexRoutingModule } from './index-routing.module';

@NgModule({
  imports: [
    CommonModule,
    IndexRoutingModule
  ],
  declarations: [
    IndexComponent
  ]
})
export class IndexModule { }
