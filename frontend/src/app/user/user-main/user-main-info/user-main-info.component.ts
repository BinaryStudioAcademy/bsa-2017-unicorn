import { Component, OnInit, Input, ViewChild, AfterViewChecked } from '@angular/core';
import { NgModel } from '@angular/forms';
import { User } from '../../../models/user';
import { AgmMap } from "@agm/core";
import { NguiMapModule, Marker } from "@ngui/map";
@Component({
  selector: 'app-user-main-info',
  templateUrl: './user-main-info.component.html',
  styleUrls: ['./user-main-info.component.sass']
})
export class UserMainInfoComponent implements OnInit {
  @Input() user: User;
  @ViewChild(AgmMap) private map: any;
    location: Location;

  lat: number = 48.464921;
  lng: number = 35.045798;
  constructor() { }
  ngOnInit() {
  }

}
