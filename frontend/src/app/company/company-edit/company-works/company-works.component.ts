import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";
import { CompanyWorks } from "../../../models/company-page/company-works.model";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";

import { PhotoService, Ng2ImgurUploader } from "../../../services/photo.service";

import { ImageCropperComponent, CropperSettings } from "ng2-img-cropper";
import {ToastsManager, Toast} from 'ng2-toastr';
import { SafeResourceUrl, DomSanitizer } from "@angular/platform-browser";
import { SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import { ConfirmModal } from '../../../confirm-modal/confirm-modal.component';
import { ImageCropperModal } from '../../../image-cropper-modal/image-cropper-modal.component';

@Component({
  selector: 'app-company-works',
  templateUrl: './company-works.component.html',
  styleUrls: ['./company-works.component.sass'],
  providers: [
    PhotoService,
    Ng2ImgurUploader
  ]
})

export class CompanyWorksComponent implements OnInit {
  workIconUrl: SafeResourceUrl;

  company: CompanyWorks;
  companyId: number;
  selectedCategory: CompanyCategory;
  selectedSubcategory: CompanySubcategory;
  subcategories: CompanySubcategory[] = [];
  work: CompanyWork = { Id: null, Description: null, Name: null, Subcategory: null, Icon: null };
  isLoaded: boolean = false;
  openedDetailedWindow: boolean = false;
  isDimmed: boolean = false;  

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,    
    private zone: NgZone,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer,
    private modalService: SuiModalService,
    private toastr: ToastsManager
  ) { }

  ngOnInit() {
    //this.initializeThisCompany(); 
    this.getCompanyAsync().then(() => {
      this.checkForWork();
    });    
  }

  getCompanyAsync(): Promise<any> {
    return this.companyService.getCompanyWorks(this.route.snapshot.params['id']).then(resp => {
      this.company = resp;
      this.companyId = this.company.Id;
    });
  }

  checkForWork() {
    let category = +this.route.snapshot.queryParams['category'];
    let subcategory = +this.route.snapshot.queryParams['subcategory'];
    let name = this.route.snapshot.queryParams['name'];
    if (category && subcategory && name) {

      this.work = {
        Id: null,
        Description: null,
        Name: name,
        Subcategory: null,
        Icon: null
      };
      this.selectedCategory = this.company.AllCategories.filter(c => c.Id === category)[0];
      this.setSubcategory(subcategory);
      if (!this.openedDetailedWindow){
        this.openedDetailedWindow = true;
      }
    }
  }

  initializeThisCompany(){
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyWorks(params['id'])).subscribe(res => {
      this.company = res;
      this.companyId = this.company.Id;
    });
  }

  setSubcategory(id: number) {
    this.subcategories = this.company.AllCategories.find(x => x.Id == this.selectedCategory.Id).Subcategories;
    this.zone.run(() => { this.selectedSubcategory = this.subcategories.filter(s => s.Id === id)[0]; });
  }

  changeCategory() {
    this.subcategories = this.company.AllCategories.find(x => x.Id == this.selectedCategory.Id).Subcategories;
    this.zone.run(() => { this.selectedSubcategory = this.subcategories[0]; });  
  }

  selectWorksRow(event: any, work: CompanyWork) {    
    if (event.target.localName !== "button" && event.target.localName !== "i") {
      this.work = work;
      this.selectedCategory = this.company.AllCategories.find(x => x.Id === work.Subcategory.Category.Id);
      this.subcategories = this.company.AllCategories.find(x => x.Id == this.selectedCategory.Id).Subcategories;
      this.zone.run(() => { this.selectedSubcategory = this.subcategories[0]; });  
      this.workIconUrl = this.buildSafeUrl(this.work.Icon);
      this.openedDetailedWindow = true;
    }
    // else {
    //   this.deleteWork(work);
    // }
  }

  openDetailedWindow() {
    this.work = {
      Id: null,
      Description: null,
      Name: null,
      Subcategory: null,
      Icon: null
    };
    this.selectedCategory = this.company.AllCategories[0];
    this.subcategories = this.company.AllCategories.find(x => x.Id == this.selectedCategory.Id).Subcategories;
    this.zone.run(() => { this.selectedSubcategory = this.subcategories[0]; });  
    if (!this.openedDetailedWindow){
      this.openedDetailedWindow = true;
    }
  }

  closeDetailedWindow() {
    this.openedDetailedWindow = false;
  }

  deleteWork(work: CompanyWork) {
    this.modalService.open(new ConfirmModal("Delete work", `Delete work ${this.work.Name}?`, "Delete"))
      .onApprove(() => {
        this.work = work;
        let companyId = this.company.Id;
        this.company = undefined;
        if (this.openedDetailedWindow) {
          this.openedDetailedWindow = false;
        }

        this.companyService.deleteCompanyWork(companyId, this.work.Id)
          .then(() => {
            this.initializeThisCompany();
            this.work = null;
            this.toastr.success('Work was deleted');
          }).catch(err => this.toastr.error('Something goes wrong', 'Error!'));
      });
  }

  saveWorkChanges() {
    if (this.selectedCategory !== undefined && this.selectedSubcategory !== undefined
      && this.work.Description !== null && this.work.Name !== null) {
        this.openedDetailedWindow = false;
        this.company = undefined;        
        this.companyService.saveCompanyWork(this.work)
          .then(() => {
            this.initializeThisCompany();
            this.toastr.success('Work was updated');
          }).catch(err => this.toastr.error('Something goes wrong', 'Error!')); ;     
    }
  }

  addWork() {
    if (this.selectedCategory !== undefined && this.selectedSubcategory !== undefined
      && this.work.Description !== null && this.work.Name !== null) {
      this.selectedCategory.Subcategories = null;
      this.selectedSubcategory.Category = this.selectedCategory;
      this.work.Subcategory = this.selectedSubcategory;
      
      this.company = undefined;
      this.openedDetailedWindow = false;
      this.companyService.addCompanyWork(this.companyId, this.work)
      .then(() => {
        this.initializeThisCompany();
        this.toastr.success('Work was added');
      }).catch(err => this.toastr.error('Something goes wrong', 'Error!'));        
    }
  }

  addOrSaveWork() {
    if (this.work.Id !== null) {
      this.saveWorkChanges();
    }
    else {
      this.addWork();
    }
  }
  
  
  buildSafeUrl(link: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustStyle(`url('${link}')`);
  }

  selectIcon(): void {
    this.modalService.open(new ImageCropperModal())
      .onApprove(result => this.work.Icon = result as string);
  }
}
