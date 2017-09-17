import { Component, OnInit } from '@angular/core';

import { SuiModal, ComponentModalConfig } from 'ng2-semantic-ui';
import { ToastsManager } from 'ng2-toastr';

import { ProfileShortInfo } from '../models/profile-short-info.model';
import { Report } from '../models/report/report.model';
import { ReportType } from '../models/report/reportType.model';

import { ReportService } from '../services/report.service';
import { AccountService } from '../services/account.service';
import { TokenHelperService } from '../services/helper/tokenhelper.service';

interface IFeedbackModalContext {
  title: string;
  feedbackType: ReportType;
  profileId: number;
  profileType: string;
  profileName: string;
}

export class FeedbackModal extends ComponentModalConfig<IFeedbackModalContext, void, void> {
  constructor(title: string, feedbackType: ReportType, profileId: number = null, profileName: string = null, profileType: string = null) {
      super(FeedbackModalComponent, { title, feedbackType, profileId, profileName, profileType });

      this.isClosable = true;
      this.isInverted = true;
      this.size = "mini";
  }
}

@Component({
  selector: 'app-feedback-modal',
  templateUrl: './feedback-modal.component.html',
  styleUrls: ['./feedback-modal.component.sass'],
  providers: [ReportService, AccountService, TokenHelperService]
})
export class FeedbackModalComponent implements OnInit {
  report: Report;
  loading: boolean;
  
  constructor(
    private reportService: ReportService,
    private accountService: AccountService,
    private tokenHelper: TokenHelperService,
    private modal: SuiModal<IFeedbackModalContext, void, void>,
    private toastr: ToastsManager) { }

  ngOnInit() {
    this.initReport();
    if (this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired()) {
      this.accountService.getShortInfo(+this.tokenHelper.getClaimByName("accountid"))
        .then(resp => this.report.Email = (resp.body as ProfileShortInfo).Email);
    }
  }

  sendMessage() {
    this.loading = true;

    this.reportService.createReport(this.report)
      .then(resp => {
        this.loading = false;
        this.toastr.success('Thank you for your report!');
        this.modal.approve(undefined);
      })
      .catch(err => {
        this.loading = false;
        this.toastr.error('Ooops! Try again');
      });
  }

  initReport(): void {
    this.report = {
      Id: null,
      Date: new Date(),
      Type: this.modal.context.feedbackType,
      Message: "",
      Email: "",
      ProfileId: this.modal.context.profileId,
      ProfileName: this.modal.context.profileName,
      ProfileType: this.modal.context.profileType
    };
  }

}
