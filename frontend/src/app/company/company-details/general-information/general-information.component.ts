import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'company-general-information',
  templateUrl: './general-information.component.html',
  styleUrls: ['./general-information.component.css']
})
export class GeneralInformationComponent implements OnInit {
@Input()
company: any;


  constructor() { }

  ngOnInit() {    
  }

}
