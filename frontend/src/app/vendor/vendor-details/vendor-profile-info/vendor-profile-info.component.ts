import { Component, OnInit, Input } from '@angular/core';
import { NgModel } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';

import { BookComponent } from '../../../book/book/book.component';

import { Vendor } from '../../../models/vendor.model';
import { Category } from "../../../models/category.model";
import { Work } from "../../../models/work.model";
import { VendorService } from "../../../services/vendor.service";
import { Subcategory } from "../../../models/subcategory.model";
import { Rating } from "../../../models/rating.model";

@Component({
  selector: 'app-vendor-profile-info',
  templateUrl: './vendor-profile-info.component.html',
  styleUrls: ['./vendor-profile-info.component.sass']
})
export class VendorProfileInfoComponent implements OnInit {
  @Input() vendor: Vendor;
  
  rating: Rating;
  workSubcategories: Subcategory[];

  constructor(private vendorService: VendorService) { }

  ngOnInit() {
    this.vendorService.getRating(this.vendor.Id)
      .then(rating => this.rating = rating);
    this.vendorService.getSubcategories(this.vendor.Id)
      .then(categories => this.workSubcategories = categories);
  }

}
