import { Component, OnInit, Input } from '@angular/core';

import { Vendor } from '../../models/vendor.model';

@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass']
})
export class VendorProfileContactsComponent implements OnInit {
  @Input() vendorId: number;
  
  constructor() { }

  ngOnInit() {
  }

}
