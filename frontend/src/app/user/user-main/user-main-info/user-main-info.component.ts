import { Component, OnInit, Input} from '@angular/core';
import { NgModel } from '@angular/forms';
import {SuiModule} from 'ng2-semantic-ui';
import { User } from '../../../models/user';
import { AgmMap } from "@agm/core";
import { NguiMapModule, Marker } from "@ngui/map";
import { UserService } from "../../../services/user.service";
import { Rating } from "../../../models/rating.model";
import { LocationService } from "../../../services/location.service";

@Component({
  selector: 'app-user-main-info',
  templateUrl: './user-main-info.component.html',
  styleUrls: ['./user-main-info.component.sass']
})
export class UserMainInfoComponent implements OnInit {
  @Input() user: User;

  lat: number = 48.464921;
  lng: number = 35.045798;
  rating: Rating = new Rating();
  constructor(private userService: UserService) {}
  ngOnInit() { 
     this.userService.getRating(this.user.Id)
     .then(resp => this.rating = resp.body as Rating);
  }
  mapClicked($event: MouseEvent){
    this.lat=$event.clientX;
    this.lng=$event.clientY;
  
}
}
