import { Component, OnInit, ChangeDetectorRef } from '@angular/core';

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
  
  constructor(
    private changeRef: ChangeDetectorRef
  ) { 
  }
  
  onSelect(event) {
    console.log(event);
  }

  ngOnInit() {
  }

}
