import { Component, OnInit, ViewChild } from '@angular/core';

import {SuiModalService, TemplateModalConfig, ModalTemplate} from 'ng2-semantic-ui';

import { ReportService } from "../../../services/report.service";
import { AccountService } from "../../../services/account.service";

import { Report } from "../../../models/report/report.model";
import { NotificationService } from "../../../services/notifications/notification.service";

export interface IContext {
  report: Report;
}

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.sass']
})
export class FeedbackComponent implements OnInit {

  @ViewChild('modalTemplate')
  public modalTemplate:ModalTemplate<IContext, string, string>
  
  constructor(
    private reportService: ReportService, 
    private accountService: AccountService,
    public modalService:SuiModalService,
    private notificationService: NotificationService
  ) { }

  reports: Report[];
  pendingReports: Report[];

  isLoaded: boolean;

  ngOnInit() {
    this.notificationService.listen<any>("RefreshAdminFeedbacks", res => {
      this.reports.push(res);
     });

    this.pendingReports = [];
    this.load();
  }

  load(): void {
    this.isLoaded = false;
    this.reportService.getReports()
      .then(resp => {
        this.reports = resp;
        this.isLoaded = true;
      })
      .catch(err => this.isLoaded = true);
  }

  openReportInModal(report: Report): void {
    const config = new TemplateModalConfig<IContext, string, string>(this.modalTemplate);
    config.context = { report: report };
    config.isClosable = true;
    config.isInverted = true;
    
    this.modalService
      .open(config)
  }

  remove(report: Report): void {
    this.pendingReports.push(report);
    this.reportService.deleteOffer(report.Id)
      .then(() => {
        this.reports.splice(this.reports.findIndex(r => r.Id === report.Id), 1);
        this.pendingReports.splice(this.pendingReports.findIndex(r => r.Id === report.Id), 1);
        var type = report.Type
      })
      .catch(err => this.pendingReports.splice(this.pendingReports.findIndex(r => r.Id === report.Id), 1));
  }
}
