import { Component, OnInit, Input } from '@angular/core';

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

  constructor(
    private locationService: LocationService, 
    private vendorService: VendorService,
    private categoryService: CategoryService,
    private workService: WorkService
  ) { }

  ngOnInit() {
    this.dataLoaded = true;   
    this.newWork = {
      CategoryId: null,
      Category: "",
      Subcategory: "",
      Description: "",
      Id: null,
      SubcategoryId: null,
      Name: ""
    };
    this.categoryService.getAll()
      .then(resp => this.categories = resp.body as Category[])
      .then(() => console.log(this.categories));
    this.workService.getAll()
      .then(resp => this.works = resp.body as Work[])
    this.locationService.getById(this.vendor.LocationId)
      .then(resp => this.location = resp.body as Location)
      .then(() => this.map = {
          center: {lat: this.location.Latitude, lng: this.location.Longitude},
          zoom: 18,    
          title: "Overcat 9000",
          label: "",
          markerPos: {lat: this.location.Latitude, lng: this.location.Longitude}
        });
  }

  onDateSelected(date: Date): void {
    console.log(date.getDate());
  }

  onSubcategorySelected(subcategory: Subcategory): void {
    if (subcategory !== undefined || subcategory !== null)
      this.subcategoryWorks = this.works
        .filter(w => w.SubcategoryId === subcategory.Id)
        .filter(w => this.vendor.Works.find(x => x.Id === w.Id) === undefined);
  }

  addWorkType(): void {
    this.newWork.CategoryId = this.selectedCategory.Id;
    this.newWork.Category = this.selectedCategory.Name;
    this.newWork.SubcategoryId = this.selectedSubcategory.Id;
    this.vendor.Works.push(this.newWork);
    this.newWork = {
      CategoryId: null,
      Category: "",
      Subcategory: "",
      Description: "",
      Id: null,
      SubcategoryId: null,
      Name: ""
    };
  }

  removeWorkType(work: Work): void {
    this.vendor.Works.splice(this.vendor.Works.findIndex(w => w.Id === work.Id), 1);
  }

  saveVendor(): void {
    this.dataLoaded = false;
    this.vendor.Birthday = this.birthday;
    this.vendorService.updateVendor(this.vendor)
      .then(resp => this.vendor = resp.body as Vendor)
      .then(() => this.dataLoaded = true);
  }

}
