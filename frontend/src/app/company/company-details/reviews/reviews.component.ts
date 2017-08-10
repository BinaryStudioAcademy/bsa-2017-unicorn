import { Component, OnInit, Input } from '@angular/core';
import { Company } from "../../../models/company.model";
import { Review } from "../../../models/review.model";

@Component({
  selector: 'company-reviews',
  templateUrl: './reviews.component.html',
  styleUrls: ['./reviews.component.css']
})
export class ReviewsComponent implements OnInit {
@Input()
reviews: Review[];
  constructor() { }

  ngOnInit() {
  }

}
