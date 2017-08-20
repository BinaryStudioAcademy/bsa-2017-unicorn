import { Component, OnInit, Input } from '@angular/core';
import { CompanyDetails } from "../../../models/company-page/company-details.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";

@Component({
  selector: 'company-general-information',
  templateUrl: './general-information.component.html',
  styleUrls: ['./general-information.component.sass']
})
export class GeneralInformationComponent implements OnInit {

company: CompanyDetails;

constructor(private companyService: CompanyService,
  private route: ActivatedRoute) { }

  ngOnInit() {    
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyDetails(params['id']))
    .subscribe(res => {
      this.company = res;
    });   
  }

}
