import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TokenHelperService } from '../../services/helper/tokenhelper.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.sass']
})
export class DashboardComponent implements OnInit {

  isVendor: boolean;
  isCompany: boolean;

  constructor(
    private route: ActivatedRoute,
    private tokenHelper: TokenHelperService
  ) {
  }

  ngOnInit() {
    this.initRole();
  }

  initRole() {
    let role = +this.tokenHelper.getClaimByName('roleid');
    this.isVendor = role === 3;
    this.isCompany = role === 4;
  }

}
