import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuiModule } from 'ng2-semantic-ui';

import { IndexComponent } from './index-component/index.component';
import { IndexRoutingModule } from './index-routing.module';
import { PopularTasksComponent } from './index-component/popular-tasks/popular-tasks.component';
import { FooterComponent } from '../footer/footer.component';

@NgModule({
  imports: [
    CommonModule,
    IndexRoutingModule,
    FormsModule,
    SuiModule
  ],
  declarations: [
    IndexComponent,
    PopularTasksComponent,
    FooterComponent
  ]
})
export class IndexModule { }
