import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { NgModel } from '@angular/forms';

import { SuiModule } from 'ng2-semantic-ui';

import { BookComponent } from '../../../book/book/book.component';

import { Vendor } from '../../../models/vendor.model';
import { Category } from "../../../models/category.model";
import { Work } from "../../../models/work.model";
import { VendorService } from "../../../services/vendor.service";
import { Subcategory } from "../../../models/subcategory.model";
import { Review } from "../../../models/review.model";

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import { ReportService } from '../../../services/report.service';
import { AccountService } from '../../../services/account.service';
import { TokenHelperService } from '../../../services/helper/tokenhelper.service';
import { ToastsManager, Toast } from 'ng2-toastr';

import { Report } from '../../../models/report/report.model';
import { ReportType } from '../../../models/report/reportType.model';
import { ProfileShortInfo } from '../../../models/profile-short-info.model';
import { FeedbackModal } from '../../../feedback-modal/feedback-modal.component';

@Component({
  selector: 'app-vendor-profile-info',
  templateUrl: './vendor-profile-info.component.html',
  styleUrls: ['./vendor-profile-info.component.sass'],
  providers: [ReportService]
})
export class VendorProfileInfoComponent implements OnInit {
  @Input() vendor: Vendor;
  @Output() notify: EventEmitter<Work[]> = new EventEmitter<Work[]>();

  rating: number;

  works: Work[];
  categoryWorks: Work[];
  workCategories: Category[];
  reviewsCount: number;
  selectedCategory: Category;
  openedCategoryDetails: boolean = false;

  constructor(
    private vendorService: VendorService,
    private modalService: SuiModalService,
    private toastr: ToastsManager
  ) { }

  ngOnInit() {
    this.vendorService.getRating(this.vendor.Id)
      .then(resp => this.rating = resp.body as number);
    this.vendorService.getReviews(this.vendor.Id)
      .then(resp => this.reviewsCount = (resp.body as Review[]).length)

    this.vendorService.getVendorWorks(this.vendor.Id)
      .then(resp => { this.works = resp.body as Work[]; this.notify.emit(this.works); })
      .then(() => this.vendorService.getCategories(this.vendor.Id))
      .then(resp => this.workCategories = resp.body as Category[]);
  }

  onCategorySelect(category: Category): void {
    if(this.selectedCategory !== category){
      setTimeout(() => {
        this.openedCategoryDetails = true;
        this.selectedCategory = category;
        this.categoryWorks = this.works.filter(w => w.CategoryId === this.selectedCategory.Id);
      }, 50); 
    }
    else{
      this.openedCategoryDetails = false;
      this.selectedCategory = undefined;
    }
  }

  closeCategoryDetails(event){     
    if(!(event.target.id.includes("divCategory") || event.target.id.includes("imgCategory") ||
      event.target.id.includes("h5Category"))){
      this.openedCategoryDetails = false;
      this.selectedCategory = undefined;
    }
  }

  getWorkIcon(work: Work): string {
    return work.Icon;
  }

  openReportModal(): void {
    this.modalService.open(new FeedbackModal("Report this vendor", ReportType.complaint, this.vendor.Id, `${this.vendor.Name} ${this.vendor.Surname}`, "vendor"));
  }

}
