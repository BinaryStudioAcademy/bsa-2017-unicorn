import { Component, OnInit } from '@angular/core';
import { CompanyDetails } from "../../../models/company-page/company-details.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";

@Component({
  selector: 'app-company-main-information',
  templateUrl: './company-main-information.component.html',
  styleUrls: ['./company-main-information.component.sass']
})
export class CompanyMainInformationComponent implements OnInit {

  company: CompanyDetails;
  isLoaded: boolean = false;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyDetails(params['id'])).subscribe(res => {
      this.company = res;
      console.log(res);
      });
  }

  save(){
    this.isLoaded = true;
    this.companyService.saveCompanyDetails(this.company).then(() => {this.isLoaded = false});
  }
  

}
