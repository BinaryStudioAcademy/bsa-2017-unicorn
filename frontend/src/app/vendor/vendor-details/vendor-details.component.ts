import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { JwtHelper } from '../../helpers/jwthelper';
import 'rxjs/add/operator/switchMap';

import { Vendor } from '../../models/vendor.model';

import { VendorService } from "../../services/vendor.service";

@Component({
  selector: 'app-vendor-details',
  templateUrl: './vendor-details.component.html',
  styleUrls: ['./vendor-details.component.sass']
})
export class VendorDetailsComponent implements OnInit { 
  vendor: Vendor;
  isGuest: boolean;
  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService
  ) {this.getCurrentRole(); }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.vendorService.getVendor(params['id']))
      .subscribe(vendor => this.vendor = vendor);
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
