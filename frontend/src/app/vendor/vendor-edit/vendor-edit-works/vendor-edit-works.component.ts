import { Component, OnInit, Input } from '@angular/core';

import { VendorService } from "../../../services/vendor.service";

import { Work } from "../../../models/work.model";

@Component({
  selector: 'app-vendor-edit-works',
  templateUrl: './vendor-edit-works.component.html',
  styleUrls: ['./vendor-edit-works.component.sass']
})
export class VendorEditWorksComponent implements OnInit {
  @Input() vendorId;

  works: Work[];


  constructor(
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    this.vendorService.getVendorWorks(this.vendorId)
      .then(resp => this.works = resp.body as Work[]);
  }

}
