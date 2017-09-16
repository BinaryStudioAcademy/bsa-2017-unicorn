import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ChartService } from '../../../services/charts/chart.service';

import { Analytics } from '../../../models/charts/charts.model';

@Component({
  selector: 'app-company-charts',
  templateUrl: './company-charts.component.html',
  styleUrls: ['./company-charts.component.sass']
})
export class CompanyChartsComponent implements OnInit {
  
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

  
    viewPie: any[] = [370, 300];
  
    // options
    showLegendPie = false;
  
    colorSchemePie = {
      domain: ['#5AA454', '#008ae6', '#AAAAAA', '#A10A28', '#C7B42C']
    };
  
    // pie
    showLabels = true;
    explodeSlices = false;
    doughnut = false;

    
    colorSchemeBar = {
      domain: ['#5AA454', '#008ae6', '#AAAAAA', '#A10A28', '#C7B42C']
    };
  //----------------------------------
  isCompanyPage: boolean = true;
  pageSelected: string = 'Company analytics';

  enabled: boolean = false;

  //Company page
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

  //Vendors page

  vendorRateOptions = [{
    id: 1,
    value: 'Rating'
  }, {
    id: 2,
    value: 'Orders count'
  }, {
    id: 3,
    value: 'Finished tasks'
  }];
  selectedVendorOpt: string = this.vendorRateOptions[0].value;
  selectedVendorOptId: number = 1;

  vendorRating: any;
  vendorOrders: any;
  vendorFinished: any;

  vendorData: any;

  

  constructor(
    private changeRef: ChangeDetectorRef,
    private chartService: ChartService,
    private route: ActivatedRoute
  ) { 
  }
  
  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
    //this.enabled = true;
    this.chartService.getCompanyCharts(this.route.snapshot.params['id']).then(resp => {
      this.analytics = resp;
      this.booksAccepted = [this.analytics.BooksAccepted];
      this.booksDeclined = [this.analytics.BooksDeclined];
      this.popularWorks = this.analytics.PopularWorks.Points;
      this.confirmedWorks = this.analytics.ConfirmedWorks.Points;
      this.declinedWorks = this.analytics.DeclinedWorks.Points;
      this.workData = this.popularWorks;

      this.vendorRating = this.analytics.VendorsByRating.Points;
      this.vendorOrders = this.analytics.VendorsByOrders.Points;
      this.vendorFinished = this.analytics.VendorsByFinished.Points;
      this.vendorData = this.vendorRating;

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

  onVendorOptionSelect() {
    let id = this.vendorRateOptions.filter(o => o.value === this.selectedVendorOpt)[0].id;
    if (id === 1) {
      this.vendorData = this.vendorRating;
    } 
    if (id === 2) {
      this.vendorData = this.vendorOrders;
    } 
    if (id === 3) {
      this.vendorData = this.vendorFinished;
    } 
  }

  formatYAxis(val) {
    let v = val as number;
    if (v % 1 !== 0) {
      return '';
    }
    return +v;
  }

  formatXAxis(val) {
    let str = val as string;
    if (str.length > 11) {
      return str.substr(0, 10).trim() + '...';
    }
    return str;
  }

}
