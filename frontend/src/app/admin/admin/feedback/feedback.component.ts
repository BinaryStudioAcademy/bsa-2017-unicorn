import { Component, OnInit } from '@angular/core';
import { ReportService } from "../../../services/report.service";
import { Report } from "../../../models/report/report.model";
import { AccountService } from "../../../services/account.service";

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.sass']
})
export class FeedbackComponent implements OnInit {

  constructor(private reportService: ReportService, private accountService: AccountService) { }

  reports: Report[];
  pendingReports: Report[];

  isLoaded: boolean;

  ngOnInit() {
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

  remove(report: Report): void {
    this.pendingReports.push(report);
    this.reportService.deleteOffer(report.Id)
      .then(() => {
        this.reports.splice(this.reports.findIndex(r => r.Id === report.Id));
        this.pendingReports.splice(this.pendingReports.findIndex(r => r.Id === report.Id));
      })
      .catch(err => this.pendingReports.splice(this.pendingReports.findIndex(r => r.Id === report.Id)));
  }
}
