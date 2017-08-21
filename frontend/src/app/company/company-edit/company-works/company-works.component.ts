import { Component, OnInit } from '@angular/core';
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";
import { CompanyWorks } from "../../../models/company-page/company-works.model";

@Component({
  selector: 'app-company-works',
  templateUrl: './company-works.component.html',
  styleUrls: ['./company-works.component.sass']
})
export class CompanyWorksComponent implements OnInit {

  company: CompanyWorks;
  selectedCategory: string;
  selectedSubcategory: string;
  subcategories: CompanySubcategory[] = [];
  workName: string;
  workDescription: string;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyWorks(params['id'])).subscribe(res => {
      this.company = res;
      // console.log(this.company);
    });
  }

  changeCategory(){
    // console.log(this.selectedCategory);
    this.selectedSubcategory = "";
    this.subcategories = this.company.AllCategories.find(x => x.Name == this.selectedCategory).Subcategories;
  }
}
