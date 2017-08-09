import { Component, OnInit, Input } from '@angular/core';
import { Vendor } from "../../../models/vendor";

@Component({
  selector: 'company-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.css']
})
export class VendorsComponent implements OnInit {
@Input()
vendors: Vendor[];

  constructor() { }

  ngOnInit() {
  }

}
