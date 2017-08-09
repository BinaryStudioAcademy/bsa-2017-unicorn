import { Component, OnInit, Input } from '@angular/core';

import { Vendor } from '../../models/vendor.model';

@Component({
  selector: 'app-vendor-profile-portfolio',
  templateUrl: './vendor-profile-portfolio.component.html',
  styleUrls: ['./vendor-profile-portfolio.component.css']
})
export class VendorProfilePortfolioComponent implements OnInit {
  @Input() vendor: Vendor;
  constructor() { }

  ngOnInit() {
  }

}