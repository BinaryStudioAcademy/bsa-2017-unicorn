import { Component, OnInit, ViewChild } from '@angular/core';
import { Company } from "../../models/company.model";
import { Review } from "../../models/review.model";
import { CompanyService } from "../../services/company.service";
import { JwtHelper } from '../../helpers/jwthelper';
import { ActivatedRoute, Params } from "@angular/router";

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.sass']
})
export class CompanyDetailsComponent implements OnInit {
  company: Company;  
  isGuest: boolean;  
  constructor(private companyService: CompanyService,
    private route: ActivatedRoute) { }

  ngOnInit() {  
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompany(params['id'])).subscribe(res => {
      this.company = res;  
      console.log(res.Reviews);
      this.company.Vendors.forEach(element => {
        if(element.Avatar == "default"){
          element.Avatar = "https://image.flaticon.com/icons/png/512/78/78373.png";
        }
      });
      // console.log(res);
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

