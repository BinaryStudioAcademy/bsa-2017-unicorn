import { Component, OnInit, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { CompanyDetails } from "../../../models/company-page/company-details.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";

import { SuiModule } from 'ng2-semantic-ui';
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
  selector: 'company-general-information',
  templateUrl: './general-information.component.html',
  styleUrls: ['./general-information.component.sass'],
  providers: [ModalService, ReportService]
})
export class GeneralInformationComponent implements OnInit {

company: CompanyDetails;
rating: number;
categories: CompanyCategory[] = [];
selectedCategory: CompanyCategory;
categoryWorks: CompanyWork[];
openedCategoryDetails: boolean = false;
@Output() notify: EventEmitter<CompanyWork[]> = new EventEmitter<CompanyWork[]>();

@ViewChild('modalTemplate')
public modalTemplate: ModalTemplate<void, {}, void>;
private activeModal: SuiActiveModal<void, {}, void>;

isLogged: boolean;
message: string;
email: string;
profileInfo: ProfileShortInfo;
loader: boolean;

constructor(
  private companyService: CompanyService,
  private route: ActivatedRoute,
  private modalService: ModalService,
  private reportService: ReportService,
  private accountService: AccountService,
  private tokenHelper: TokenHelperService,
  private toastr: ToastsManager
  ) { }

  ngOnInit() {        
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyDetails(params['id']))
    .subscribe(res => {
      this.company = res;      
      this.company.Works.forEach(work => {
        if(this.categories.find(x => x.Id === work.Subcategory.Category.Id) === undefined){
          this.categories.push(work.Subcategory.Category);
        }
      });
      this.notify.emit(this.company.Works);
      this.companyService.getCompanyRating(this.company.Id).
      then(resp => this.rating = resp.body as number);      
    });   
  }

  onCategorySelect(category: CompanyCategory): void {
    if(this.selectedCategory !== category){
      setTimeout(() => {
        
        this.openedCategoryDetails = true;
        this.selectedCategory = category;
        this.categoryWorks = this.company.Works.filter(w => w.Subcategory.Category.Id === this.selectedCategory.Id);
        //console.log(this.categoryWorks);
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

  getWorkIcon(work: CompanyWork): string {
    return work.Icon === null ? work.Subcategory.Category.Icon : work.Icon;
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
        ProfileId: this.company.Id,
        ProfileType: 'company'
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
