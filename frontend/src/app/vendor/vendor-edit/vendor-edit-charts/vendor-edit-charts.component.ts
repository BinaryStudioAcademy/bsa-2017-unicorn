import { Component, OnInit, Input } from '@angular/core';

import { ChartService } from '../../../services/charts/chart.service';

import { Analytics } from '../../../models/charts/charts.model';

@Component({
  selector: 'app-vendor-edit-charts',
  templateUrl: './vendor-edit-charts.component.html',
  styleUrls: ['./vendor-edit-charts.component.sass']
})
export class VendorEditChartsComponent implements OnInit {

  @Input() private vendorId: number;

  view: any[] = [850, 300];
  
    // options
    showXAxis = true;
    showYAxis = true;
    gradient = true;
    showLegend = false;
    showXAxisLabel = true;
    xAxisLabel = '';
    showYAxisLabel = false;
    yAxisLabel = 'Books accepted';
  
    yAxisLabel2 = 'Books declined';
  
    colorScheme = {
      domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
    };
  
    colorScheme2 = {
      domain: ['#ff9999', '#5AA454', '#C7B42C', '#AAAAAA']
    };
  
    // line, area
    autoScale = false;
  
    
      viewPie: any[] = [405, 300];
    
      // options
      showLegendPie = false;
    
      colorSchemePie = {
        domain: ['#008ae6', '#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
      };
    
      // pie
      showLabels = true;
      explodeSlices = false;
      doughnut = false;
  
      
      colorSchemeBar = {
        domain: ['#008ae6', '#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
      };
      
    //----------------------------------
    isCompanyPage: boolean = true;
    pageSelected: string = 'Company analytics';

    enabled: boolean = false;
  
    workOptions = [{
      id: 1,
      value: 'Popularity'
    }, {
      id: 2,
      value: 'Success works count'
    }, {
      id: 3,
      value: 'Declined works count'
    }];
  
    selectedType: string = this.workOptions[0].value;
    selectedTypeId: number = 1;
  
    analytics: Analytics;
  
    booksAccepted: any;
    booksDeclined: any;
  
    popularWorks: any;
    confirmedWorks: any;
    declinedWorks: any;
  
    workData: any;

  constructor(
    private chartService: ChartService
  ) { }

  ngOnInit() {
    this.chartService.getVendorCharts(this.vendorId).then(resp => {
      this.analytics = resp;
      this.booksAccepted = [this.analytics.BooksAccepted];
      this.booksDeclined = [this.analytics.BooksDeclined];
      this.popularWorks = this.analytics.PopularWorks.Points;
      this.confirmedWorks = this.analytics.ConfirmedWorks.Points;
      this.declinedWorks = this.analytics.DeclinedWorks.Points;
      this.workData = this.popularWorks;

      console.log(resp);
    });
  }

  onTypeSelect() {
    let id = this.workOptions.filter(o => o.value === this.selectedType)[0].id;
    if (id === 1) {
      this.workData = this.popularWorks;
    } 
    if (id === 2) {
      this.workData = this.confirmedWorks;
    } 
    if (id === 3) {
      this.workData = this.declinedWorks;
    } 
  }

  formatYAxis(val) {
    if (val % 1 !== 0) {
      return '';
    }
    return Number(val);
  }

  formatXAxis(val) {
    let str = val as string;
    if (str.length > 11) {
      return str.substr(0, 10).trim() + '...';
    }
    return str;
  }

}
