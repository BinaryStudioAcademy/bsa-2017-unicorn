import { Component, OnInit, Input,ViewChild, AfterViewChecked } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';
import {SuiModule} from 'ng2-semantic-ui';
import { User } from '../../../models/user';
import { AgmMap } from "@agm/core";
import { NguiMapModule, Marker } from "@ngui/map";
import { UserService } from "../../../services/user.service";
import { LocationService } from "../../../services/location.service";
import { Review} from "../../../models/review.model"
@Component({
  selector: 'app-user-main-info',
  templateUrl: './user-main-info.component.html',
  styleUrls: ['./user-main-info.component.sass']
})
export class UserMainInfoComponent implements OnInit {
  @Input() user: User;
  @ViewChild(AgmMap) private map: any;
  rating: number;
  reviewsCount: number;
  constructor(private userService: UserService) {}
  ngOnInit() {
     this.userService.getRating(this.user.Id)
     .then(resp => this.rating = resp.body as number);
     this.userService.getReviews(this.user.Id)
     .then(resp => this.reviewsCount = (resp.body as Review[]).length)
  }
}
