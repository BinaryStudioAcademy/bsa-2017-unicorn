import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ImageCropperComponent, CropperSettings } from "ng2-img-cropper";
import { SafeResourceUrl, DomSanitizer } from "@angular/platform-browser";
import { SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import { ToastsManager, Toast } from 'ng2-toastr';
import { ToastOptions } from 'ng2-toastr';

import { VendorService } from "../../../services/vendor.service";
import { CategoryService } from "../../../services/category.service";
import { PhotoService, Ng2ImgurUploader } from "../../../services/photo.service";

import { Work } from "../../../models/work.model";
import { Category } from "../../../models/category.model";
import { Subcategory } from "../../../models/subcategory.model";
import { ImageCropperModal } from '../../../image-cropper-modal/image-cropper-modal.component';
import { ConfirmModal } from '../../../confirm-modal/confirm-modal.component';

@Component({
  selector: 'app-vendor-edit-works',
  templateUrl: './vendor-edit-works.component.html',
  styleUrls: ['./vendor-edit-works.component.sass'],
  providers: [
    PhotoService,
    Ng2ImgurUploader
  ]
})
export class VendorEditWorksComponent implements OnInit {
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton: boolean = false;
  workIconUrl: SafeResourceUrl;
  uploading: boolean = false;
  loader:boolean = false;

  @Input() vendorId;

  works: Work[];
  categories: Category[];
  subcategories: Subcategory[];
  selectedCategory: Category;
  selectedSubcategory: Subcategory;
  selectedWork: Work;
  pendingWorks: Work[];

  isEditOpen: boolean;

  constructor(
    private vendorService: VendorService,
    private categoryService: CategoryService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer,
    private modalService: SuiModalService,
    private toastr: ToastsManager,
    private route: ActivatedRoute
  ) { }

  ngOnInit() {
    this.isEditOpen = false;
    this.vendorService.getVendorWorks(this.vendorId)
      .then(resp => this.works = resp.body as Work[]);
    this.categoryService.getAll()
      .then(resp => this.categories = resp.body as Category[])
      .then(() => this.checkForWork());
    this.subcategories = [];
    this.pendingWorks = [];
    this.clearSelectedWork();
  }

  checkForWork() {
    let category = +this.route.snapshot.queryParams['category'];
    let subcategory = +this.route.snapshot.queryParams['subcategory'];
    let name = this.route.snapshot.queryParams['name'];
    if (category && subcategory && name) {
      this.selectedWork.Name = name;
      this.selectedCategory = this.categories.filter(c => c.Id === category)[0];
      this.onCategorySelect();
      this.selectedSubcategory = this.subcategories.filter(s => s.Id === subcategory)[0];

      this.isEditOpen = true;
    }
    
    this.loader = true;
  }

  editToggle(): void {
    this.isEditOpen = !this.isEditOpen;
    this.clearSelectedWork();
  }

  onWorkSelect(work: Work): void {
    this.selectedWork = work;
    this.selectedCategory = this.categories.find(c => c.Id == this.selectedWork.CategoryId);
    this.onCategorySelect();
    this.selectedSubcategory = this.subcategories.find(c => c.Id == this.selectedWork.SubcategoryId);
    this.workIconUrl = this.buildSafeUrl(this.selectedWork.Icon);
    this.isEditOpen = true;
  }

  onCategorySelect(): void {
    this.selectedSubcategory = null;
    this.subcategories = this.selectedCategory.Subcategories;
  }

  isWorkValid(): boolean {
    return this.selectedCategory 
      && this.selectedSubcategory 
      && this.selectedWork.Name !== ''
  }

  createWork(): void {
    this.selectedWork.CategoryId = this.selectedCategory.Id;
    this.selectedWork.SubcategoryId = this.selectedSubcategory.Id;

    this.selectedWork.Subcategory = this.selectedSubcategory.Name;
    this.selectedWork.Category = this.selectedCategory.Name;

    let work = this.selectedWork;

    if (work.Icon === null || work.Icon === '' || work.Icon === undefined) {
      work.Icon = this.selectedCategory.Icon;
    }

    this.pendingWorks.push(work);
    this.works.push(work)
    this.vendorService.postVendorWork(this.vendorId, this.selectedWork)
      .then(resp => {
        this.pendingWorks.splice(this.pendingWorks.findIndex(w => w === work), 1);
        work.Id = (resp.body as Work).Id;
        this.toastr.success('Changes were saved', 'Success!')
      })
      .catch(err => {
        this.pendingWorks.splice(this.pendingWorks.findIndex(w => w === work), 1);
        this.toastr.error('Sorry, something went wrong', 'Error!');
      });
    this.clearSelectedWork();
    this.isEditOpen = false;
  }

  updateWork(): void {
    this.selectedWork.CategoryId = this.selectedCategory.Id;
    this.selectedWork.SubcategoryId = this.selectedSubcategory.Id;
    this.vendorService.updateVendorWork(this.vendorId, this.selectedWork.Id, this.selectedWork)
      .then(() => this.toastr.success('Changes were saved', 'Success!'))
      .catch(() => this.toastr.error('Sorry, something went wrong', 'Error!'));
    this.clearSelectedWork();
    this.isEditOpen = false;
  }

  removeWork(work: Work): void {
    this.modalService.open(new ConfirmModal("Delete work", `Delete work ${work.Name}?`, "Delete"))
      .onApprove(() => {
        if (this.selectedWork.Id === work.Id) {
          this.isEditOpen = false;
          this.clearSelectedWork();
        }

        this.pendingWorks.push(work);

        this.vendorService.removeVendorWork(this.vendorId, work.Id, work)
          .then(resp => {
            this.pendingWorks.splice(this.pendingWorks.findIndex(w => w === work), 1);
            this.works.splice(this.works.findIndex(w => w.Id === work.Id));
            this.toastr.success('Changes were saved', 'Success!');
          })
          .catch(() => {
            this.pendingWorks.splice(this.pendingWorks.findIndex(w => w === work), 1);
            this.toastr.error('Sorry, something went wrong', 'Error!');
          });
      });
  }

  isWorkPending(work: Work): boolean {
    return this.pendingWorks.includes(work);
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

  selectIcon(): void {
    this.modalService.open(new ImageCropperModal())
      .onApprove(result => this.selectedWork.Icon = result as string);
  }
}
