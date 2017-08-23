import { Component, OnInit, Input } from '@angular/core';
import { CompanyDetails } from "../../../models/company-page/company-details.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyCategory } from "../../../models/company-page/company-category.model";

@Component({
  selector: 'company-general-information',
  templateUrl: './general-information.component.html',
  styleUrls: ['./general-information.component.sass']
})
export class GeneralInformationComponent implements OnInit {

company: CompanyDetails;
rating: number;
categories: CompanyCategory[] = [];

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

      console.log(this.categories);
    });   
  }

}
