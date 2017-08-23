import { Component, OnInit, Input } from '@angular/core';
import { CompanyDetails } from "../../../models/company-page/company-details.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";

@Component({
  selector: 'company-general-information',
  templateUrl: './general-information.component.html',
  styleUrls: ['./general-information.component.sass']
})
export class GeneralInformationComponent implements OnInit {

company: CompanyDetails;
rating: number;
categories: CompanyCategory[] = [];
selectedCategory: CompanyCategory;
categoryWorks: CompanyWork[];
openedCategoryDetails: boolean = false;

constructor(private companyService: CompanyService,
  private route: ActivatedRoute) { }

  ngOnInit() {    
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyDetails(params['id']))
    .subscribe(res => {
      this.company = res;

      this.company.Works.forEach(work => {
        this.categories.push(work.Subcategory.Category);
      });

      this.companyService.getCompanyRating(this.company.Id).
      then(resp => this.rating = resp.body as number);      
    });   
  }

  onCategorySelect(category: CompanyCategory): void {
    this.openedCategoryDetails = true;
    this.selectedCategory = category;
    this.categoryWorks = this.company.Works.filter(w => w.Subcategory.Category.Id === this.selectedCategory.Id);
  }

  closeCategoryDetails(){
    this.openedCategoryDetails = false;
    this.selectedCategory = undefined;
  }

}
