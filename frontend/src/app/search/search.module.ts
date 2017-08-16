import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SuiModule } from 'ng2-semantic-ui';
import { SearchComponent } from './search-component/search.component';

import { SearchRoutingModule } from './search-routing.module';

import { Company } from '../models/company.model';
import { Review } from '../models/review.model';
import { Vendor } from '../models/vendor';
import { environment } from '../../environments/environment';
import { CompanyService } from '../services/company.service';
import { NguiMapModule } from '@ngui/map';
import { MapComponent } from '../map/map.component';

@NgModule({
  imports: [
    CommonModule,
    SearchRoutingModule,
    FormsModule,
    SuiModule
  ],
  declarations: [SearchComponent],
  providers: [
    CompanyService
  ]
})
export class SearchModule { }
