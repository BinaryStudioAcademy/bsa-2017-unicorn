import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { NguiMapModule, Marker } from "@ngui/map";
import { SuiModule } from 'ng2-semantic-ui';

import { Location } from "../../../models/location.model"
import { Vendor } from "../../../models/vendor.model";
import { MapModel } from "../../../models/map.model";
import { Work } from "../../../models/work.model";
import { Category } from "../../../models/category.model";
import { Subcategory } from "../../../models/subcategory.model";

import { VendorService } from "../../../services/vendor.service";
import { LocationService } from "../../../services/location.service";
import { CategoryService } from "../../../services/category.service";
import { WorkService } from "../../../services/work.service";

@Component({
  selector: 'app-vendor-edit-info',
  templateUrl: './vendor-edit-info.component.html',
  styleUrls: ['./vendor-edit-info.component.sass']
})
export class VendorEditInfoComponent implements OnInit {
  @Input() vendor: Vendor;
  
  birthday: Date;
  location: Location;
  map: MapModel;
  dataLoaded: boolean;
  
  newWork: Work;
  categories: Category[];
  selectedCategory: Category;
  selectedSubcategory: Subcategory;
  works: Work[];
  subcategoryWorks: Work[];

  @ViewChild('vendorForm') public vendorForm: NgForm;
  
  constructor(
    private locationService: LocationService, 
    private vendorService: VendorService,
    private categoryService: CategoryService,
    private workService: WorkService
  ) { }

  ngOnInit() {
    this.dataLoaded = true;   
    this.categoryService.getAll()
      .then(resp => this.categories = resp.body as Category[]);
    this.workService.getAll()
      .then(resp => this.works = resp.body as Work[])
   
  }

  onDateSelected(date: Date): void {
    console.log(date.getDate());
  }

  addWorkType(): void {
    if (this.newWork.Name !== "") {
      this.newWork.CategoryId = this.selectedCategory.Id;
      this.newWork.Category = this.selectedCategory.Name;
      this.newWork.SubcategoryId = this.selectedSubcategory.Id;
    }
  }

  saveVendor(): void {
    if (this.vendorForm.invalid) {
      return;
    }
    this.dataLoaded = false;
    this.vendor.Birthday = this.birthday;
    this.vendorService.updateVendor(this.vendor)
      .then(resp => this.vendor = resp.body as Vendor)
      .then(() => this.dataLoaded = true);
  }

}
