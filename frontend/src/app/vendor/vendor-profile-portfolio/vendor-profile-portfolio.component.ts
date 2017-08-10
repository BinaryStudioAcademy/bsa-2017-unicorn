import { Component, OnInit, Input } from '@angular/core';

import {SuiModule} from 'ng2-semantic-ui';

import { VendorService } from "../../services/vendor.service";

import { Vendor } from '../../models/vendor.model';
import { PortfolioItem } from '../../models/portfolio-item.model';

@Component({
  selector: 'app-vendor-profile-portfolio',
  templateUrl: './vendor-profile-portfolio.component.html',
  styleUrls: ['./vendor-profile-portfolio.component.sass']
})
export class VendorProfilePortfolioComponent implements OnInit {
  @Input() vendorId: number;
  portfolio: PortfolioItem[];

  constructor(private vendorService: VendorService) { }

  ngOnInit() {
    this.portfolio = this.vendorService.getVendorPorfolio(this.vendorId);
  }

}
