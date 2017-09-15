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
import { ModalService } from '../../../services/modal/modal.service';
import { ReportService } from '../../../services/report.service';
import { AccountService } from '../../../services/account.service';
import { TokenHelperService } from '../../../services/helper/tokenhelper.service';
import { ToastsManager, Toast } from 'ng2-toastr';

import { Report } from '../../../models/report/report.model';
import { ReportType } from '../../../models/report/reportType.model';
import { ProfileShortInfo } from '../../../models/profile-short-info.model';

@Component({
  selector: 'app-vendor-profile-info',
  templateUrl: './vendor-profile-info.component.html',
  styleUrls: ['./vendor-profile-info.component.sass'],
  providers: [ModalService, ReportService]
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

  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<void, {}, void>;
  private activeModal: SuiActiveModal<void, {}, void>;

  isLogged: boolean;
  message: string;
  email: string;
  profileInfo: ProfileShortInfo;
  loader: boolean;

  constructor(
    private vendorService: VendorService,
    private modalService: ModalService,
    private reportService: ReportService,
    private accountService: AccountService,
    private tokenHelper: TokenHelperService,
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

  openModal() {
    this.getAccount();
    this.message = undefined;
    this.activeModal = this.modalService.openModal(this.modalTemplate, ModalSize.Mini);
  }

  sendMessage(formData) {
    if (formData.valid) {
      this.loader = true;
      const report: Report = {
        Id: 1,
        Date: new Date(),
        Type: ReportType.complaint,
        Message: this.message,
        Email: this.email,
        ProfileId: this.vendor.Id,
        ProfileName: `${this.vendor.Name} ${this.vendor.Surname}`,
        ProfileType: 'vendor'
      };

      this.reportService.createReport(report).then(resp => {
        this.loader = false;
        this.toastr.success('Thank you for your report!');
        this.activeModal.approve('approved');
      }).catch(err => {
        this.loader = false;
        this.toastr.error('Ooops! Try again');
      });
    }
  }

  getAccount() {
    this.isLogged = this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired();
    if (this.isLogged) {
      this.accountService.getShortInfo(+this.tokenHelper.getClaimByName('accountid'))
      .then(resp => {
        if (resp !== undefined) {
          this.profileInfo = resp.body as ProfileShortInfo;
          this.email = this.profileInfo.Email;
        }
      });
    } else {
      this.email = undefined;
    }
  }
}
