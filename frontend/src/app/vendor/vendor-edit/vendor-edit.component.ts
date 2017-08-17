import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';

import 'rxjs/add/operator/switchMap';

import { Vendor } from '../../models/vendor.model';

import { VendorService } from "../../services/vendor.service";

@Component({
  selector: 'app-vendor-edit',
  templateUrl: './vendor-edit.component.html',
  styleUrls: ['./vendor-edit.component.sass']
})
export class VendorEditComponent implements OnInit {
  vendor: Vendor;

  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.vendorService.getVendor(params['id']))
      .subscribe(resp => this.vendor = resp.body as Vendor);
  }
}