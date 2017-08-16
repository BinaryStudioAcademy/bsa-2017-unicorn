import { Component, OnInit, Input } from '@angular/core';
import { Company } from "../../../models/company.model";

@Component({
  selector: 'company-general-information',
  templateUrl: './general-information.component.html',
  styleUrls: ['./general-information.component.sass']
})
export class GeneralInformationComponent implements OnInit {
@Input()
company: Company;
isCompanyNull: boolean = false;
reviewsLength: number = 0;


  constructor() { }

  ngOnInit() {    
    if(this.company === null){
      this.isCompanyNull = true;
    }
    else{
      this.isCompanyNull = false;
      if(this.company.Reviews === null){
        this.reviewsLength = 0;
      }
      else{
        this.reviewsLength = this.company.Reviews.length;
      }
    }      
  }

}
