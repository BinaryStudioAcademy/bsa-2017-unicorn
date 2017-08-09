import { Component, OnInit, Input } from '@angular/core';

import { Vendor } from '../../models/vendor.model';

@Component({
  selector: 'app-vendor-profile-reviews',
  templateUrl: './vendor-profile-reviews.component.html',
  styleUrls: ['./vendor-profile-reviews.component.css']
})
export class VendorProfileReviewsComponent implements OnInit {
  @Input() vendor: Vendor;
  constructor() { }

  ngOnInit() {
  }

}
