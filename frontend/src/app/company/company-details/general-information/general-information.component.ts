import { Component, OnInit, Input } from '@angular/core';
import { Company } from "../../../models/company.model";

@Component({
  selector: 'company-general-information',
  templateUrl: './general-information.component.html',
  styleUrls: ['./general-information.component.css']
})
export class GeneralInformationComponent implements OnInit {
@Input()
company: Company;


  constructor() { }

  ngOnInit() {    
  }

}
