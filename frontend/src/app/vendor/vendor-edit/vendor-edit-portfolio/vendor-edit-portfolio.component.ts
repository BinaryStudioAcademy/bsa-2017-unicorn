import { Component, OnInit, Input, ViewChild } from '@angular/core';

import {SuiModule} from 'ng2-semantic-ui';
import {SuiModalService, TemplateModalConfig, ModalTemplate} from 'ng2-semantic-ui';

import { VendorService } from "../../../services/vendor.service";

import { Vendor } from '../../../models/vendor.model';
import { PortfolioItem } from '../../../models/portfolio-item.model';
import { History } from '../../../models/history';

export interface IContext {
    data:string;
}

@Component({
  selector: 'app-vendor-edit-portfolio',
  templateUrl: './vendor-edit-portfolio.component.html',
  styleUrls: ['./vendor-edit-portfolio.component.sass']
})
export class VendorEditPortfolioComponent implements OnInit {
  @Input() private vendorId: number;
  portfolio: PortfolioItem[];
  history: History;

  @ViewChild('modalTemplate')
  public modalTemplate:ModalTemplate<IContext, string, string>

  constructor(private vendorService: VendorService, private modalService: SuiModalService) { }

  ngOnInit() {
    this.vendorService.getVendorPorfolio(this.vendorId)
      .then(resp => this.portfolio = resp.body as PortfolioItem[]);
  }

  public open(dynamicContent:string = "Example") {
    const config = new TemplateModalConfig<IContext, string, string>(this.modalTemplate);

    config.closeResult = "closed!";
    config.context = { data: dynamicContent };

    this.modalService
        .open(config)
        .onApprove(result => { /* approve callback */ })
        .onDeny(result => { /* deny callback */});
}

}

