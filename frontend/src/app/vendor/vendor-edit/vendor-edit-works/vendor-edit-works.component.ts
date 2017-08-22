import { Component, OnInit, Input, ViewChild } from '@angular/core';

import { ImageCropperComponent, CropperSettings } from "ng2-img-cropper";
import { SafeResourceUrl, DomSanitizer } from "@angular/platform-browser";
import { SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

import { VendorService } from "../../../services/vendor.service";
import { ModalService } from "../../../services/modal/modal.service";
import { CategoryService } from "../../../services/category.service";
import { PhotoService, Ng2ImgurUploader } from "../../../services/photo.service";

import { Work } from "../../../models/work.model";
import { Category } from "../../../models/category.model";
import { Subcategory } from "../../../models/subcategory.model";

export interface IContext { }

@Component({
  selector: 'app-vendor-edit-works',
  templateUrl: './vendor-edit-works.component.html',
  styleUrls: ['./vendor-edit-works.component.sass'],
  providers: [
        PhotoService,
        Ng2ImgurUploader,
        ModalService]
})
export class VendorEditWorksComponent implements OnInit {
  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<IContext, string, string>;
  
  private activeModal: SuiActiveModal<IContext, {}, string>;

  @ViewChild('cropper', undefined)
  cropper: ImageCropperComponent;
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton:boolean = false;
  workIconUrl: SafeResourceUrl;
  uploading: boolean;
  
  modalSize: string;
  cropperSettings: CropperSettings;
  data: any;
  file: File;
  imageUploaded: boolean;  

  @Input() vendorId;

  works: Work[];
  categories: Category[];
  subcategories: Subcategory[];
  selectedCategory: Category;
  selectedSubcategory: Subcategory;
  selectedWork: Work;

  editOpen: boolean;

  constructor(
    private vendorService: VendorService,
    private categoryService: CategoryService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer,
    private suiModalService: SuiModalService,
    private modalService: ModalService,    
  ) {
      this.cropperSettings = modalService.cropperSettings;
      this.data = {};
      this.imageUploaded = false;
   }

  ngOnInit() {
    this.editOpen = false;
    this.vendorService.getVendorWorks(this.vendorId)
      .then(resp => this.works = resp.body as Work[]);
    this.categoryService.getAll()
      .then(resp => this.categories = resp.body as Category[]);
    this.subcategories = [];
    this.clearSelectedWork();
  }

  editToggle(): void {
    this.editOpen = !this.editOpen;
    this.clearSelectedWork();
  }

  onWorkSelect(work: Work): void {
    this.selectedWork = work;
    this.selectedCategory = this.categories.find(c => c.Id == this.selectedWork.CategoryId);
    this.onCategorySelect();
    this.selectedSubcategory = this.subcategories.find(c => c.Id == this.selectedWork.SubcategoryId);
    this.workIconUrl = this.buildSafeUrl(this.selectedWork.Icon);
    this.editOpen = true;
  }

  onCategorySelect(): void {
    this.subcategories = this.selectedCategory.Subcategories;
  }

  createWork(): void {
    this.selectedWork.CategoryId = this.selectedCategory.Id;
    this.selectedWork.SubcategoryId = this.selectedSubcategory.Id;
    this.vendorService.postVendorWork(this.vendorId, this.selectedWork)
      .then(resp => this.works = resp.body as Work[]);
    this.clearSelectedWork();
  }

  updateWork(): void {
    this.selectedWork.CategoryId = this.selectedCategory.Id;
    this.selectedWork.SubcategoryId = this.selectedSubcategory.Id;
    this.vendorService.updateVendorWork(this.vendorId, this.selectedWork.Id, this.selectedWork);
    this.clearSelectedWork();
  }

  removeWork(work: Work): void {
    this.vendorService.removeVendorWork(this.vendorId, work.Id, work)
      .then(resp => this.works = resp.body as Work[]);
  }

  clearSelectedWork(): void {
    this.selectedCategory = null;
    this.selectedSubcategory = null;
    this.selectedWork = {
      Id: null,
      Category: "",
      Subcategory: "",
      CategoryId: null,
      SubcategoryId: null,
      Description: "",
      Name: "",
      Icon: ""
    };
  }

  buildSafeUrl(link: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustStyle(`url('${link}')`);
  }

  fileChangeListener($event) {
    var image: any = new Image();
    if ($event.target !== undefined)
      this.file = $event.target.files[0];
    var myReader:FileReader = new FileReader();
    var that = this;
    myReader.onloadend = function (loadEvent:any) {
        image.src = loadEvent.target.result;
        that.cropper.setImage(image);

    };
    this.imageUploaded = true;
    myReader.readAsDataURL(this.file);
  }

  fileSaveListener(){
    if (!this.data)
    {
      console.log("file not upload");
      return;
    }
    this.photoService.uploadToImgur(this.file)
      .then(resp => {
        let path = resp;
        console.log(path);
        console.log(this.modalTemplate);
        this.activeModal.deny('');        
        this.selectedWork = this.data.image;
        })
      .catch(err => {
        console.log(err);
      });
  }

  public openModal() {
    this.modalService.openModal(this.modalTemplate, this.activeModal);
  }

}
