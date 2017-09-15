import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { Report } from '../models/report/report.model';


@Injectable()
export class ReportService {

  constructor(private dataService: DataService) {
    this.dataService.setHeader('Content-Type', 'application/json');
  }

  getReports(): Promise<Report[]> {
    return this.dataService.getRequest(`report`);
  }

  getReport(id: number): Promise<Report> {
    return this.dataService.getRequest(`report/${id}`);
  }

  createReport(report: Report): Promise<any> {
    return this.dataService.postRequest('report', report);
  }

  updateReport(report: Report): Promise<any> {
    return this.dataService.putRequest('report', report);
  }

  deleteOffer(id: number): Promise<any> {
    return this.dataService.deleteRequest(`report/${id}`);
  }

}
