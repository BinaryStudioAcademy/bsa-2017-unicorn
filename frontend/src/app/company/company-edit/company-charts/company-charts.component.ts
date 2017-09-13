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

  single = [
    {
      "name": "Germany",
      "value": 8940000
    },
    {
      "name": "USA",
      "value": 5000000
    },
    {
      "name": "France",
      "value": 7200000
    }
  ];
  
  multi = [
    {
      "name": "books",
      "series": [
        {
          "name": "March",
          "value": 8
        },
        {
          "name": "April",
          "value": 6
        },
        {
          "name": "May",
          "value": 5
        },
        {
          "name": "June",
          "value": 11
        },
        {
          "name": "July",
          "value": 9
        },
        {
          "name": "August",
          "value": 7
        },
        {
          "name": "September",
          "value": 6
        },
        {
          "name": "October",
          "value": 3
        },
        {
          "name": "November",
          "value": 2
        },
        {
          "name": "December",
          "value": 6
        },
        {
          "name": "Janary",
          "value": 13
        },
        {
          "name": "February",
          "value": 11
        }
      ]
    }
  ];
  
  declinedData = [
    {
      "name": "declined",
      "series": [
        {
          "name": "March",
          "value": 0
        },
        {
          "name": "April",
          "value": 5
        },
        {
          "name": "May",
          "value": 7
        },
        {
          "name": "June",
          "value": 10
        },
        {
          "name": "July",
          "value": 8
        },
        {
          "name": "August",
          "value": 6
        },
        {
          "name": "September",
          "value": 5
        },
        {
          "name": "October",
          "value": 2
        },
        {
          "name": "November",
          "value": 1
        },
        {
          "name": "December",
          "value": 5
        },
        {
          "name": "Janary",
          "value": 12
        },
        {
          "name": "February",
          "value": 10
        }
      ]
    }
  ];
  
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




  dataPie = [
    {
      "name": "Germany",
      "value": 8940000
    },
    {
      "name": "USA",
      "value": 5000000
    },
    {
      "name": "France",
      "value": 7200000
    }
  ];
  
    viewPie: any[] = [400, 300];
  
    // options
    showLegendPie = false;
  
    colorSchemePie = {
      domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
    };
  
    // pie
    showLabels = true;
    explodeSlices = false;
    doughnut = false;

    
    colorSchemeBar = {
      domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
    };
  //----------------------------------
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
    this.chartService.getCompanyCharts(this.route.snapshot.params['id']).then(resp => {
      this.analytics = resp;
      this.booksAccepted = [this.analytics.BooksAccepted];
      this.booksDeclined = [this.analytics.BooksDeclined];
      this.popularWorks = this.analytics.PopularWorks.Points;
      this.confirmedWorks = this.analytics.ConfirmedWorks.Points;
      this.declinedWorks = this.analytics.DeclinedWorks.Points;
      console.log(resp);
    });
  }

  getBooksAccepted() {
    return [this.analytics.BooksAccepted];
  }

  onTypeSelect() {
    this.selectedTypeId = this.workOptions.filter(o => o.value === this.selectedType)[0].id;
  }

}
