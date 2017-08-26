import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";
import { CompanyWorks } from "../../../models/company-page/company-works.model";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";

import { ModalService } from "../../../services/modal/modal.service";
import { PhotoService, Ng2ImgurUploader } from "../../../services/photo.service";

import { ImageCropperComponent, CropperSettings } from "ng2-img-cropper";
import { SafeResourceUrl, DomSanitizer } from "@angular/platform-browser";
import { SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

@Component({
  selector: 'app-company-works',
  templateUrl: './company-works.component.html',
  styleUrls: ['./company-works.component.sass'],
  providers: [
    PhotoService,
    Ng2ImgurUploader,
    ModalService]
})

export class CompanyWorksComponent implements OnInit {
  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<void, {}, void>;
  private activeModal: SuiActiveModal<void, {}, void>;

  @ViewChild('cropper', undefined)
  cropper: ImageCropperComponent;
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton: boolean = false;
  workIconUrl: SafeResourceUrl;
  uploading: boolean;

  modalSize: string;
  cropperSettings: CropperSettings;
  data: any;
  file: File;
  imageUploaded: boolean;


  company: CompanyWorks;
  companyId: number;
  selectedCategory: CompanyCategory;
  selectedSubcategory: CompanySubcategory;
  subcategories: CompanySubcategory[] = [];
  work: CompanyWork = { Id: null, Description: null, Name: null, Subcategory: null, Icon: null };
  isLoaded: boolean = false;
  openedDetailedWindow: boolean = false;
  isDimmed: boolean = false;
  dataLoaded: boolean = true;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,    
    private zone: NgZone,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer,
    private suiModalService: SuiModalService,
    private modalService: ModalService,) {
      this.cropperSettings = modalService.cropperSettings;
      this.data = {};
      this.imageUploaded = false; 
     }

  ngOnInit() {
    this.initializeThisCompany();       
  }

  initializeThisCompany(){
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyWorks(params['id'])).subscribe(res => {
      this.company = res;
      this.companyId = this.company.Id;
    });
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
    else {
      this.deleteWork(work);
    }
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
    let companyId = this.company.Id;
    this.company = undefined;
    if (this.openedDetailedWindow){
      this.openedDetailedWindow = false;
    }

    this.companyService.deleteCompanyWork(companyId, work.Id)
      .then(() => {
        this.initializeThisCompany();  
        this.work = null;        
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
          });     
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
      });        
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

  fileChangeListener($event) {
    var image: any = new Image(); 
    console.log($event)
    this.file = $event.target.files[0];   
    var myReader: FileReader = new FileReader();
    var that = this;
    myReader.onloadend = function (loadEvent: any) {
      image.src = loadEvent.target.result;
      that.cropper.setImage(image);      
    };
    this.imageUploaded = true;
    myReader.readAsDataURL(this.file);
    // if($event.target !== undefined){
      
      
    // }
    
  }

  fileSaveListener() {
    if (!this.data) {
      console.log("file can't be loaded");
      return;
    }
    if(!this.file){
      return;
    }
    this.dataLoaded = false;
    this.photoService.uploadToImgur(this.file)
      .then(resp => {        
        this.dataLoaded = true;
        this.work.Icon = this.data.image;
        this.activeModal.deny(null);
      })
      .catch(err => {
        console.log(err);
      });
  }

  public openModal() {
    this.activeModal = this.modalService.openModal(this.modalTemplate);
  }
}
