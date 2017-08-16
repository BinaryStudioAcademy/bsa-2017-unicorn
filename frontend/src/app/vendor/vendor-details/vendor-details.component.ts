import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import 'rxjs/add/operator/switchMap';

import { Vendor } from '../../models/vendor.model';

import { VendorService } from "../../services/vendor.service";

@Component({
  selector: 'app-vendor-details',
  templateUrl: './vendor-details.component.html',
  styleUrls: ['./vendor-details.component.sass']
})
export class VendorDetailsComponent implements OnInit { 
  vendor: Vendor;

  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    this.vendorService.getVendor(1)
      .then(vendor => {
        this.vendor = vendor; 
        console.log(vendor)
      });
  }

}
