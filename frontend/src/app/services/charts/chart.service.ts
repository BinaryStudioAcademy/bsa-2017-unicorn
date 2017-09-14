import { Injectable } from '@angular/core';

import { DataService } from '../data.service';

import { Analytics } from '../../models/charts/charts.model';

@Injectable()
export class ChartService {

  constructor(private dataService: DataService) { }

  getCompanyCharts(id: number): Promise<Analytics> {
    return this.dataService.getRequest(`company/${id}/charts`);
  }

  getVendorCharts(id: number): Promise<Analytics> {
    return this.dataService.getRequest(`vendors/${id}/charts`);
  }

}
