import { Component, OnInit, Input } from '@angular/core';
import { Vendor } from "../../../models/vendor";
import { Company } from "../../../models/company.model";

@Component({
  selector: 'company-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.sass']
})
export class VendorsComponent implements OnInit {
@Input()
vendors: Vendor[];

  constructor() { }

  ngOnInit() {
    console.log(this.vendors);
  }

}
