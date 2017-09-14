import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-company-calendar',
  templateUrl: './company-calendar.component.html',
  styleUrls: ['./company-calendar.component.sass']
})
export class CompanyCalendarComponent implements OnInit {
@Input()
accountId: number;

  constructor() { }

  ngOnInit() {
  }

}
