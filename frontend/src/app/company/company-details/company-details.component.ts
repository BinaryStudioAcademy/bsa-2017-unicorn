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
  company: Promise<Company>;
  companyAvatar: string;

  constructor(private companyService: CompanyService) { }

  ngOnInit() {  
    this.company = this.companyService.getCompany(1).then(res => {
      this.companyAvatar = res.Avatar;
      console.log(res);
      return res;});        
    
  }    
}

