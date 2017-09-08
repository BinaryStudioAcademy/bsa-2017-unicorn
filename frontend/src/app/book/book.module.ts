import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { NguiMapModule } from "@ngui/map";
import { environment } from "../../environments/environment";

import { LocationService } from "../services/location.service";

import { SuiModule } from 'ng2-semantic-ui';
import { BookOrderService } from '../services/book-order.service'
import { BookComponent } from './book/book.component';

@NgModule({
  imports: [
    NguiMapModule.forRoot({
      apiUrl: 'https://maps.google.com/maps/api/js?key=' + environment.googleMapsKey +
      '&libraries=visualization,places,drawing'
    }),
    CommonModule,
    FormsModule,
    SuiModule
  ],
  declarations: [BookComponent],
  exports: [
    BookComponent
  ],
  providers: [
    BookOrderService,
    LocationService
  ]
})
export class BookModule { }
