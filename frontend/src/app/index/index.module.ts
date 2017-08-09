import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuiModule } from 'ng2-semantic-ui';

import { IndexComponent } from './index-component/index.component';
import { IndexRoutingModule } from './index-routing.module';

@NgModule({
  imports: [
    CommonModule,
    IndexRoutingModule,
    FormsModule,
    SuiModule
  ],
  declarations: [
    IndexComponent
  ]
})
export class IndexModule { }
