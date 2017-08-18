import { Component, OnInit } from '@angular/core';
import { Company } from "../../models/company.model";
import { CompanyService } from "../../services/company.service";
import { Params, ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.sass']
})
export class CompanyEditComponent implements OnInit {
  isDimmed: boolean = false;
  company: Company;  
  
  constructor(private companyService: CompanyService,
    private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompany(params['id'])).subscribe(res => {
      this.company = res;      
      // console.log(res);
      });        
  }

}
