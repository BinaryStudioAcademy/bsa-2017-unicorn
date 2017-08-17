import { Component, OnInit, Input } from '@angular/core';
import { Company } from "../../../models/company.model";
import { Review } from "../../../models/review.model";

@Component({
  selector: 'company-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.sass']
})
export class ReviewsComponent implements OnInit {
@Input()
reviews: Review[];
isReviewsEmpty: boolean = true;
  constructor() { }

  ngOnInit() {
    if(this.reviews.length == 0){
      this.isReviewsEmpty = true;
    }
    else{
      this.isReviewsEmpty = false;
    }
  }

}
