import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyService } from "../../../services/company-services/company.service";
import { CompanyVendors } from "../../../models/company-page/company-vendors.model";
import { Vendor } from "../../../models/company-page/vendor";

@Component({
  selector: 'app-company-vendors',
  templateUrl: './company-vendors.component.html',
  styleUrls: ['./company-vendors.component.sass']
})
export class CompanyVendorsComponent implements OnInit {  
  company: CompanyVendors;     
  isLoaded: boolean = false; 
  openedDetailedWindow: boolean = false;  
  allVendors: Vendor[];
  selectedVendors: Vendor[] = [];
  selectedVendor: Vendor;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private zone: NgZone) { }

  ngOnInit() { 
    this.initializeThisCompany();   
  }  

  initializeThisCompany(){
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyVendors(params['id'])).subscribe(res => {
      this.company = res;   
      this.allVendors = this.company.AllVendors;  
    });
  }

  changeVendor(){
    this.selectedVendors.push(this.selectedVendor);
    this.allVendors = this.allVendors.filter(x => x.Id !== this.selectedVendor.Id);
    this.zone.run(() => { this.selectedVendor = null; });  
  }

  openDetailedWindow(){
    if(!this.openedDetailedWindow)  {
      this.openedDetailedWindow = true;  
      this.selectedVendors = [];
    }    
  }

  closeDetailedWindow(){
    this.openedDetailedWindow = false;  
    this.allVendors = this.company.AllVendors;
    this.selectedVendors = undefined;
    this.zone.run(() => { this.selectedVendor = null; });  
  }

  deleteVendor(vendor: Vendor){     
    if(this.openedDetailedWindow){
      this.openedDetailedWindow = false; 
    }    
    this.selectedVendors = undefined;
    this.zone.run(() => { this.selectedVendor = null; }); 

    let companyId = this.company.Id;
    this.company = undefined;  
    this.companyService.deleteCompanyVendor(companyId, vendor.Id)
      .then(() => {
        this.initializeThisCompany();   
      });     
  }

  deleteSelectedVendor(vendor: Vendor){
    this.selectedVendors = this.selectedVendors.filter(x => x.Id !== vendor.Id);
    this.zone.run(() => { this.selectedVendor = null; });
    this.allVendors.push(vendor);
  }

  addVendors(){      
    this.selectedVendors.forEach(vendor => {this.company.Vendors.push(vendor);});      
    this.saveCompanyVendors();  
    this.selectedVendors = undefined;
    this.zone.run(() => { this.selectedVendor = null; });  
    this.company = undefined;    
    this.openedDetailedWindow = false;     
  }

  saveCompanyVendors(){
    this.isLoaded = true;    
    this.companyService.addCompanyVendors(this.company).then(() => {
      this.isLoaded = false;      
      this.initializeThisCompany();
    });
  }
}