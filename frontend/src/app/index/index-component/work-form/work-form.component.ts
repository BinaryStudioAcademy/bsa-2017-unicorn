import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Work } from "../../../models/work.model";
import { Category } from '../../../models/category.model';
import { Subcategory } from '../../../models/subcategory.model';
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";
import { CompanyWorks } from "../../../models/company-page/company-works.model";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";

import { CategoryService } from '../../../services/category.service';
import { TokenHelperService } from '../../../services/helper/tokenhelper.service';
import { CompanyService } from "../../../services/company-services/company.service";
import { VendorService } from "../../../services/vendor.service";

@Component({
  selector: 'app-work-form',
  templateUrl: './work-form.component.html',
  styleUrls: ['./work-form.component.sass']
})
export class WorkFormComponent implements OnInit {

  categories: Category[] = [];
  subcategories: Subcategory[] = [];

  selectedCategory: Category;
  selectedSubcategory: Subcategory;
  serviceName: string;

  role: string;
  performerId: number;

  constructor(
    private router: Router,
    private companyService: CompanyService,
    private vendorService: VendorService,
    private categoryService: CategoryService,
    private tokenHelper: TokenHelperService
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.categoryService.getAll().then(resp => {
      this.categories = resp.body as Category[];
      
      console.log('categ', this.categories);
    }).catch(err => {
      console.log(err);
    });

    this.role = this.tokenHelper.getRoleName();
    this.performerId = +this.tokenHelper.getClaimByName('profileid');
  }

  onCategorySelect() {
    this.subcategories = this.selectedCategory.Subcategories;
  }

  getWork(): Work {
    let work: Work = {
      Id: null,
      Category: this.selectedCategory.Name,
      Subcategory: this.selectedSubcategory.Name,
      CategoryId: this.selectedCategory.Id,
      SubcategoryId: this.selectedSubcategory.Id,
      Description: "",
      Name: this.serviceName,
      Icon: ""
    };
    return work;
  }

  convertWork(work: Work): CompanyWork {
    let workC: CompanyWork = {
      Id: null,
      Description: null,
      Name: null,
      Subcategory: null,
      Icon: null
    };

    return null;
  }

  createWork() {
    this.router.navigate([`${this.role}/${this.performerId}/edit`], {
      queryParams: {
        tab: 'works',
        name: this.serviceName,
        category: this.selectedCategory.Id,
        subcategory: this.selectedSubcategory.Id
      }
    });
    //this.role === 'vendor' ? this.createVendorWork() : this.createCompanyWork();
  }

  createVendorWork() {

  }

  createCompanyWork() {
    this.companyService.addCompanyWork(this.performerId, this.convertWork(this.getWork()));
  }

}
