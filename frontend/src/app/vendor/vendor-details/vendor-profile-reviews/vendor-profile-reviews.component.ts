import { Component, OnInit, Input, AfterContentInit } from '@angular/core';

import { ReviewService } from '../../../services/review.service';

import { Review } from '../../../models/review.model';
import { VendorService } from "../../../services/vendor.service";

@Component({
  selector: 'app-vendor-profile-reviews',
  templateUrl: './vendor-profile-reviews.component.html',
  styleUrls: ['./vendor-profile-reviews.component.sass']
})
export class VendorProfileReviewsComponent implements OnInit, AfterContentInit {
  @Input() private vendorId: number;
  
  reviews: Review[];

  constructor(private vendorService: VendorService) { }

  ngAfterContentInit(): void {
    this.vendorService.getReviews(this.vendorId)
      .then(resp => this.reviews = resp.body as Review[]);      
  }

  ngOnInit() {
  }

}
