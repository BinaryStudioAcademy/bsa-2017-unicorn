import { Component, OnInit, Input } from '@angular/core';
import { Company } from "../../../models/company.model";
import { CompanyService } from "../../../services/company.service";

@Component({
  selector: 'app-company-main-information',
  templateUrl: './company-main-information.component.html',
  styleUrls: ['./company-main-information.component.sass']
})
export class CompanyMainInformationComponent implements OnInit {
@Input()
  company: Company;
  isLoaded: boolean = false;

  constructor(private companyService: CompanyService) { }

  ngOnInit() {
  }

  save(){
    this.isLoaded = true;
    this.companyService.saveCompany(this.company).then(() => {this.isLoaded = false});
  }
  

}
