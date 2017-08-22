import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../../models/user';

@Component({
  selector: 'app-user-main-reviews',
  templateUrl: './user-main-reviews.component.html',
  styleUrls: ['./user-main-reviews.component.css']
})
export class UserMainReviewsComponent implements OnInit {

  @Input() user: User;
  constructor() { }

  ngOnInit() {
  }

}
