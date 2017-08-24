import { Component, OnInit, ViewChild } from '@angular/core';
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";
import { CompanyWorks } from "../../../models/company-page/company-works.model";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";
import { ModalTemplate, SuiModalService, TemplateModalConfig } from "ng2-semantic-ui";

@Component({
  selector: 'app-company-works',
  templateUrl: './company-works.component.html',
  styleUrls: ['./company-works.component.sass']
})

export class CompanyWorksComponent implements OnInit {

  company: CompanyWorks;
  selectedCategory: CompanyCategory;
  selectedSubcategory: CompanySubcategory;
  subcategories: CompanySubcategory[] = [];
  work: CompanyWork = { Id: null, Description: null, Name: null, Subcategory: null, Icon: null };
  isLoaded: boolean = false;
  openedDetailedWindow: boolean = false;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    public modalService: SuiModalService) { }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.companyService.getCompanyWorks(params['id'])).subscribe(res => {
        this.company = res;
      });
  }

  changeCategory() {
    this.subcategories = this.company.AllCategories.find(x => x.Id == this.selectedCategory.Id).Subcategories;
    this.selectedSubcategory = this.subcategories[0];
  }

  selectWorksRow(event: any, work: CompanyWork) {
    if (event.target.localName === "td") {
      this.work = {
        Id: work.Id,
        Description: work.Description,
        Name: work.Name,
        Subcategory: work.Subcategory,
        Icon: null
      };
      this.selectedCategory = this.company.AllCategories.find(x => x.Id === work.Subcategory.Category.Id);
      this.subcategories = this.company.AllCategories.find(x => x.Id == this.selectedCategory.Id).Subcategories;
      this.selectedSubcategory = this.subcategories[0];
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
    this.selectedSubcategory = this.subcategories[0];
    if (!this.openedDetailedWindow){
      this.openedDetailedWindow = true;
    }
  }

  closeDetailedWindow() {
    this.openedDetailedWindow = false;
  }

  deleteWork(work: CompanyWork) {
    this.company.Works = this.company.Works.filter(x => x.Id !== work.Id);
    this.work = null;
    this.saveCompanyWorks();
    this.company = undefined;
    if (this.openedDetailedWindow)
      this.openedDetailedWindow = !this.openedDetailedWindow;
  }

  saveWorkChanges() {
    if (this.selectedCategory !== undefined && this.selectedSubcategory !== undefined
      && this.work.Description !== null && this.work.Name !== null) {
      this.company.Works.splice(this.company.Works.findIndex(x => x.Id === this.work.Id), 1, this.work);
      this.saveCompanyWorks();
      this.company = undefined;
    }
  }

  addWork() {
    if (this.selectedCategory !== undefined && this.selectedSubcategory !== undefined
      && this.work.Description !== null && this.work.Name !== null) {
      this.selectedCategory.Subcategories = null;
      this.selectedSubcategory.Category = this.selectedCategory;
      this.work.Subcategory = this.selectedSubcategory;
      this.company.Works.push(this.work);
      this.saveCompanyWorks();
      this.openedDetailedWindow = false;
      this.company = undefined;
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

  saveCompanyWorks() {
    this.isLoaded = true;
    this.companyService.saveCompanyWorks(this.company).then(() => {
      this.isLoaded = false;
      this.ngOnInit();
    });
  }
}