import { Component, OnInit, Input, AfterContentInit } from '@angular/core';
import { User } from '../../../models/user';

import { Review } from '../../../models/review.model';
import { UserService } from "../../../services/user.service";
@Component({
  selector: 'app-user-main-reviews',
  templateUrl: './user-main-reviews.component.html',
  styleUrls: ['./user-main-reviews.component.sass']
})
export class UserMainReviewsComponent implements OnInit, AfterContentInit  {

  @Input() user: User;
  reviews: Review[];

  isReviewsEmpty: boolean;
  isLoaded: boolean;

  constructor(private userService: UserService) { }

  ngOnInit() {
  }
  ngAfterContentInit(): void {
    this.userService.getReviews(this.user.Id)
      .then(resp => {
        this.isLoaded = true;
        this.reviews = resp.body as Review[];
        if (this.reviews.length === 0) {
          this.isReviewsEmpty = true;
        }
      });
  }

}
