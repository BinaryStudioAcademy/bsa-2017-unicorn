import { Component, OnInit, ViewChild } from '@angular/core';
import { Company } from "../../models/company.model";
import { Review } from "../../models/review.model";
import { CompanyService } from "../../services/company.service";

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.sass']
})
export class CompanyDetailsComponent implements OnInit {
  company: Company;  

  constructor(private companyService: CompanyService) { }

  ngOnInit() {  
    this.companyService.getCompany(2).then(res => {
      this.company = res;      
      console.log(res);
      });        
    
  }    
}

