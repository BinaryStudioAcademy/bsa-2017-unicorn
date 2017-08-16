import { Component, OnInit } from '@angular/core';
import { Company } from "../../models/company.model";
import { CompanyService } from "../../services/company.service";

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.sass']
})
export class CompanyEditComponent implements OnInit {
  isDimmed: boolean = false;
  company: Company;
  constructor(private companyService: CompanyService) { }

  ngOnInit() {
    this.companyService.getCompany(2).then(res => {
      this.company = res;      
      console.log(res);
      });    
  }

}
