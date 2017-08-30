import { Component, OnInit } from '@angular/core';
import { Review } from "../../models/review.model";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyShort } from "../../models/company-page/company-short.model";
import { CompanyService } from "../../services/company-services/company.service";
import { TokenHelperService } from '../../services/helper/tokenhelper.service';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.sass']
})
export class CompanyDetailsComponent implements OnInit {
  company: CompanyShort;
  isGuest: boolean;
  isUser: boolean;
  tabActive: boolean = false;
  routePath: string;
  routeid: number;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private tokenHelperService: TokenHelperService) {
    this.routePath = this.route.root.snapshot.firstChild.url[0].path;
    this.routeid = +this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.companyService.getCompanyShort(params['id']))
      .subscribe(res => {
        this.company = res;
        //console.log(this.company.Name); 
      });
    if (this.route.snapshot.queryParams['tab'] === 'reviews') {
      this.tabActive = true;
    }   
    this.getCurrentRole();
  }

  getCurrentRole() {
    if (this.tokenHelperService.getToken() === null) {
      this.isGuest = true;
      this.isUser = false;
      return;
    }
    const userRoleId = +this.tokenHelperService.getClaimByName('roleid');
    this.isGuest = userRoleId === 1;
    this.isUser = userRoleId === 2;
  }
}