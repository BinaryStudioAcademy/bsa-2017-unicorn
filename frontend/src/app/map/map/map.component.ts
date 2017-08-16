import { Component, OnInit, Input } from '@angular/core';

import { MapModel } from "../../models/map.model";

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.sass']
})
export class MapComponent implements OnInit {
@Input() inputMap: MapModel;
  constructor() { }

  ngOnInit() {    
  }

}
