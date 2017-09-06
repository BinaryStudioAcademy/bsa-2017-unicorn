import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SuiModule } from 'ng2-semantic-ui';
import { SearchRoutingModule } from './search-routing.module';
import { NguiMapModule } from '@ngui/map/dist';
import { MapModule } from '../map/map.module';
import { MdlModule } from '@angular-mdl/core';
import { ClickOutsideModule } from 'ng-click-outside';

import { SearchComponent } from './search-component/search.component';
import { Review } from '../models/review.model';
import { environment } from '../../environments/environment';

import { CompanyService } from '../services/company-services/company.service';
import { SearchService } from '../services/search.service';
import { CategoryService } from '../services/category.service';


@NgModule({
  imports: [
    NguiMapModule.forRoot({
      apiUrl: 'https://maps.google.com/maps/api/js?key=' + environment.googleMapsKey +
      '&libraries=visualization,places,drawing&language=en&region=GB'
    }),
    SearchRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SuiModule,
    MapModule,
    CommonModule,
    MdlModule,
    ClickOutsideModule
  ],
  declarations: [
    SearchComponent,
  ],
  providers: [
    CompanyService,
    SearchService,
    CategoryService,
  ]
})
export class SearchModule { }
