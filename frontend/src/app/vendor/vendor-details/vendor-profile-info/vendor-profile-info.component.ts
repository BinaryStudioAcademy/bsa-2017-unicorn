import { Component, OnInit, Input } from '@angular/core';
import { NgModel } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';

import { BookComponent } from '../../../book/book/book.component';

import { Vendor } from '../../../models/vendor.model';
import { Category } from "../../../models/category.model";
import { Work } from "../../../models/work.model";
import { VendorService } from "../../../services/vendor.service";

@Component({
  selector: 'app-vendor-profile-info',
  templateUrl: './vendor-profile-info.component.html',
  styleUrls: ['./vendor-profile-info.component.sass']
})
export class VendorProfileInfoComponent implements OnInit {
  @Input() vendor: any;
  
  rating: any;
  workCategories: Category[];

  constructor(private vendorService: VendorService) { }

  ngOnInit() {
    this.vendorService.getRating(1)
      .then(rating => this.rating = rating);
    // this.workCategories = [];
    // this.vendor.workList.forEach(w => this.workCategories.push(w.category));
    // select only unique categories
  }

}
