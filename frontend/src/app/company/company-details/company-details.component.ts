import { Component, OnInit, ViewChild } from '@angular/core';
import { Review } from "../../models/review.model";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyShort } from "../../models/company-page/company-short.model";
import { CompanyService } from "../../services/company-services/company.service";
import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { CompanyWork } from "../../models/company-page/company-work.model";
import { BookComponent } from '../../book/book/book.component';

import { AuthenticationEventService } from '../../services/events/authenticationevent.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-company-details',
  templateUrl: './company-details.component.html',
  styleUrls: ['./company-details.component.sass']
})
export class CompanyDetailsComponent implements OnInit {
  @ViewChild('bookComponent') bookComponent: BookComponent;

  company: CompanyShort;
  isGuest: boolean;
  isUser: boolean;
  tabActive: boolean = false;
  routePath: string;
  routeid: number;
  works: CompanyWork[];
  selectedWorkId: number;

  onLogIn: Subscription;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private authEventService: AuthenticationEventService,
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

    if (this.route.snapshot.queryParams['work']) {
      this.selectedWorkId = +this.route.snapshot.queryParams['work'];
    }

    this.getCurrentRole();

    this.onLogIn = this.authEventService.loginEvent$
      .subscribe(() => {
        this.isGuest = !this.isGuest;
        this.isUser = !this.isUser;
      });
  }

  ngOnDestroy() {
    this.onLogIn.unsubscribe();
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

  onWorksLoaded(works: CompanyWork[]) {
    this.works = works;
    if (this.isUser) {
      let work = this.works.find(x => x.Id === this.selectedWorkId);
      this.bookComponent.selectWork(work);
    }
  }
}