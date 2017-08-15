import { Component, OnInit, Input } from '@angular/core';

import { ReviewService } from '../../../services/review.service';

import { Review } from '../../../models/review.model';
import { Vendor } from "../../../models/vendor";

@Component({
  selector: 'app-vendor-profile-reviews',
  templateUrl: './vendor-profile-reviews.component.html',
  styleUrls: ['./vendor-profile-reviews.component.sass']
})
export class VendorProfileReviewsComponent implements OnInit {
  @Input() private vendor: Vendor;
  
  reviews: Review[];

  constructor(private reviewService: ReviewService) { }

  ngOnInit() {
  }

}
