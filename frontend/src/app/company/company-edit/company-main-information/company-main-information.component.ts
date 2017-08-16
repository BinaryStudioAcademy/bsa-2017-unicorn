import { Component, OnInit, Input } from '@angular/core';
import { Company } from "../../../models/company.model";

@Component({
  selector: 'app-company-main-information',
  templateUrl: './company-main-information.component.html',
  styleUrls: ['./company-main-information.component.sass']
})
export class CompanyMainInformationComponent implements OnInit {
@Input()
  company: Company;
  constructor() { }

  ngOnInit() {
  }

}
