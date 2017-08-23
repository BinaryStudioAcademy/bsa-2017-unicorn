import { Component, OnInit, ViewChild } from '@angular/core';
import { SuiModalService, ModalTemplate, TemplateModalConfig } from "ng2-semantic-ui/dist";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyService } from "../../../services/company-services/company.service";
import { CompanyVendors } from "../../../models/company-page/company-vendors.model";
import { Vendor } from "../../../models/company-page/vendor";


export interface IContext {
  data:Vendor;
}


@Component({
  selector: 'app-company-vendors',
  templateUrl: './company-vendors.component.html',
  styleUrls: ['./company-vendors.component.sass']
})
export class CompanyVendorsComponent implements OnInit {
  @ViewChild('modalTemplate')
  public modalTemplate:ModalTemplate<IContext, Vendor, string>

  company: CompanyVendors;     
  isLoaded: boolean = false; 
  openedDetailedWindow: boolean = false;  
  selectedVendor: Vendor;


  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    public modalService:SuiModalService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyVendors(params['id'])).subscribe(res => {
      this.company = res;      
    });
  }

  public openModal(vendor:Vendor) {
    const config = new TemplateModalConfig<IContext, Vendor, string>(this.modalTemplate);

    config.context = { data: vendor };

    this.modalService
        .open(config)
        .onApprove(result => { this.deleteVendor(vendor); this.selectedVendor = undefined; })
        .onDeny(result => { });
  }

  openDetailedWindow(){
    if(!this.openedDetailedWindow)  
      this.openedDetailedWindow = true;      
  }

  closeDetailedWindow(){
    this.openedDetailedWindow = false;  
    this.selectedVendor = undefined;
  }

  deleteVendor(vendor: Vendor){      
    this.company.Vendors =  this.company.Vendors.filter(x => x.Id !== vendor.Id);   
    this.saveCompanyVendors();
    if(this.openedDetailedWindow)  
      this.openedDetailedWindow = !this.openedDetailedWindow;
  }

  addVendor(){
    if(this.company.Vendors.find(x => x.Id === this.selectedVendor.Id) === undefined){
      this.company.Vendors.push(this.selectedVendor);
      this.saveCompanyVendors();
    }
  }


  saveCompanyVendors(){
    this.isLoaded = true;
    this.companyService.saveCompanyVendors(this.company).then(() => {this.isLoaded = false;});
  }


}
