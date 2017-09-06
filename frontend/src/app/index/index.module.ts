import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuiModule } from 'ng2-semantic-ui';
import { ClickOutsideModule } from 'ng-click-outside';

import { IndexComponent } from './index-component/index.component';
import { IndexRoutingModule } from './index-routing.module';
import { PopularTasksComponent } from './index-component/popular-tasks/popular-tasks.component';
// import { FooterComponent } from '../footer/footer.component';
import { SearchPipe } from './search-pipe/search.pipe';

import { CompanyService } from "../services/company-services/company.service";
import { VendorService } from "../services/vendor.service";
import { DataService } from '../services/data.service';
import { PopularService } from '../services/popular.service';
import { CategoryService } from '../services/category.service';
import { TokenHelperService } from '../services/helper/tokenhelper.service';
import { WorkFormComponent } from './index-component/work-form/work-form.component';

@NgModule({
  imports: [
    CommonModule,
    IndexRoutingModule,
    FormsModule,
    SuiModule,
    ClickOutsideModule
  ],
  declarations: [
    IndexComponent,
    PopularTasksComponent,
    // FooterComponent,
    SearchPipe,
    WorkFormComponent
  ],
  providers: [
    PopularService,
    DataService,
    TokenHelperService,
    CategoryService,
    VendorService,
    CompanyService
  ]
})
export class IndexModule { }
