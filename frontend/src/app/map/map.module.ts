import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';
import { NguiMapModule } from "@ngui/map/dist";
import { environment } from "../../environments/environment";

import { MapComponent } from './map/map.component';

@NgModule({
  imports: [
	NguiMapModule
		.forRoot({apiUrl: `https://maps.google.com/maps/api/js?key=${environment.googleMapsKey}`}),
    CommonModule,
    FormsModule,
    SuiModule
  ],
  declarations: [MapComponent],
  exports: [
    MapComponent
  ]
})

export class MapModule { }
