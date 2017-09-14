import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-vendor-edit-calendar',
  templateUrl: './vendor-edit-calendar.component.html',
  styleUrls: ['./vendor-edit-calendar.component.sass']
})
export class VendorEditCalendarComponent implements OnInit {
@Input()
accountId: number;


  constructor() { }

  ngOnInit() {
  }

}
