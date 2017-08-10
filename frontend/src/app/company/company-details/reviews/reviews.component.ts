import { Component, OnInit, Input } from '@angular/core';
import { Company } from "../../../models/company";
import { Review } from "../../../models/review";

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
