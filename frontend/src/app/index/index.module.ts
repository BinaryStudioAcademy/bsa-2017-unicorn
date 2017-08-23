import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuiModule } from 'ng2-semantic-ui';

import { IndexComponent } from './index-component/index.component';
import { IndexRoutingModule } from './index-routing.module';
import { PopularTasksComponent } from './index-component/popular-tasks/popular-tasks.component';
// import { FooterComponent } from '../footer/footer.component';
import { SearchPipe } from './search-pipe/search.pipe';

import { DataService } from '../services/data.service';
import { PopularService } from '../services/popular.service';

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
    // FooterComponent,
    SearchPipe
  ],
  providers: [
    PopularService,
    DataService
  ]
})
export class IndexModule { }
