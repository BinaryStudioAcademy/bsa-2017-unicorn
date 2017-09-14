import { Component, OnInit, ViewChild } from '@angular/core';

import { SuiModule } from 'ng2-semantic-ui';
import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import {ToastsManager, Toast} from 'ng2-toastr';

import { ModalService } from '../services/modal/modal.service';
import { ReportService } from '../services/report.service';
import { AccountService } from '../services/account.service';
import { TokenHelperService } from '../services/helper/tokenhelper.service';


import { Report } from '../models/report/report.model';
import { ReportType } from '../models/report/reportType.model';
import { ProfileShortInfo } from '../models/profile-short-info.model';


@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.sass'],
  providers: [ModalService, ReportService]
})
export class FooterComponent implements OnInit {
  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<void, {}, void>;
  private activeModal: SuiActiveModal<void, {}, void>;

  isLogged: boolean;
  message: string;
  email: string;
  profileInfo: ProfileShortInfo;
  loader: boolean;

  constructor(
    private modalService: ModalService,
    private reportService: ReportService,
    private accountService: AccountService,
    private tokenHelper: TokenHelperService,
    private toastr: ToastsManager
    ) {  }

  ngOnInit() { }

  openModal() {
    this.getAccount();
    this.message = undefined;
    this.activeModal = this.modalService.openModal(this.modalTemplate, ModalSize.Mini);
  }

  sendMessage(formData) {
    if (formData.valid) {
      this.loader = true;
      const report: Report = {
        Id: 1,
        Date: new Date(),
        Type: ReportType.feedback,
        Message: this.message,
        Email: this.email,
        CustomerId: null,
        VendorId: null,
        CompanyId: null,
      };

      this.reportService.createReport(report).then(resp => {
        this.loader = false;
        this.toastr.success('Thank you for your feedback!');
        this.activeModal.approve('approved');
      }).catch(err => {
        this.loader = false;
        this.toastr.error('Ooops! Try again');
      });
    }
  }

  getAccount() {
    this.isLogged = this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired();
    if (this.isLogged) {
      this.accountService.getShortInfo(+this.tokenHelper.getClaimByName('accountid'))
      .then(resp => {
        if (resp !== undefined) {
          this.profileInfo = resp.body as ProfileShortInfo;
          this.email = this.profileInfo.Email;
        }
      });
    } else {
      this.email = undefined;
    }
  }
}
