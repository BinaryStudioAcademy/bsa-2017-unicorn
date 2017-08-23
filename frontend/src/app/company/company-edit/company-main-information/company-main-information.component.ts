import { Component, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { CompanyDetails } from "../../../models/company-page/company-details.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { MapModel } from "../../../models/map.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";

@Component({
  selector: 'app-company-main-information',
  templateUrl: './company-main-information.component.html',
  styleUrls: ['./company-main-information.component.sass']
})
export class CompanyMainInformationComponent implements OnInit {

  company: CompanyDetails;
  isLoaded: boolean = false;
  map: MapModel;  

  @ViewChild('companyForm') public companyForm: NgForm;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyDetails(params['id'])).subscribe(res => {
      this.company = res;

      this.map = {
        center: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude},
        zoom: 18,    
        title: this.company.Name,
        label: this.company.Name,
        markerPos: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude}    
      };        
  });
}

  save(){
    if (this.companyForm.invalid) {
      return;
    }
    this.isLoaded = true;
    this.companyService.saveCompanyDetails(this.company).then(() => {this.isLoaded = false});
  }
  

}
