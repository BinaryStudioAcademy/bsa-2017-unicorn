import { Component, OnInit, ViewChild } from '@angular/core';
import { Company } from "../../models/company.model";
import { Review } from "../../models/review.model";
import { CompanyService } from "../../services/company.service";
import { JwtHelper } from '../../helpers/jwthelper';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.sass']
})
export class CompanyDetailsComponent implements OnInit {
  company: Company;  
  isGuest: boolean;
  constructor(private companyService: CompanyService) { }

  ngOnInit() {  
    this.companyService.getCompany(2).then(res => {
      this.company = res;      
      console.log(res);
      });        

    this.getCurrentRole();
  }    
  getCurrentRole()
  {
    let token = localStorage.getItem('token');
    if(token===null)
     { 
       this.isGuest=true;
       return;
     }
    const userClaims = new JwtHelper().decodeToken(token);
    if(userClaims['roleid']!=1)
        this.isGuest=false; else
    this.isGuest=true;
  }
}

