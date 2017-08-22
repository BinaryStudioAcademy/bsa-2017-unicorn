import { Component, OnInit, Input } from '@angular/core';

import {SuiModule} from 'ng2-semantic-ui';

import { VendorService } from "../../../services/vendor.service";

import { Vendor } from '../../../models/vendor.model';
import { PortfolioItem } from '../../../models/portfolio-item.model';
import { History } from '../../../models/history';
import { VendorHistory } from "../../../models/vendor-history.model";

@Component({
  selector: 'app-vendor-edit-portfolio',
  templateUrl: './vendor-edit-portfolio.component.html',
  styleUrls: ['./vendor-edit-portfolio.component.sass']
})
export class VendorEditPortfolioComponent implements OnInit {
  @Input() private vendorId: number;
  portfolio: PortfolioItem[];
  history: VendorHistory[];

  constructor(
    private vendorService: VendorService
  ) { }

  ngOnInit() {
    this.vendorService.getVendorPorfolio(this.vendorId)
      .then(resp => this.portfolio = resp.body as PortfolioItem[]);
    this.vendorService.getVendorHistory(this.vendorId)
      .then(resp => this.history = resp.body as VendorHistory[])
  }
}

