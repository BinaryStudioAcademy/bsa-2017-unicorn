import { Component, OnInit, Input } from '@angular/core';

import { ReviewService } from '../../services/review.service';

import { Review } from '../../models/review.model';

@Component({
  selector: 'app-vendor-profile-reviews',
  templateUrl: './vendor-profile-reviews.component.html',
  styleUrls: ['./vendor-profile-reviews.component.css']
})
export class VendorProfileReviewsComponent implements OnInit {
  @Input() vendorId: number;
  
  reviews: Review[];

  constructor(private reviewService: ReviewService) { }

  ngOnInit() {
    this.reviews = this.reviewService.getVendorReviews(this.vendorId);
  }

}
