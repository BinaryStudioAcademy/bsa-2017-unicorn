import { Component, OnInit } from '@angular/core';
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanySubcategory } from "../../../models/company-page/company-subcategory.model";
import { CompanyWorks } from "../../../models/company-page/company-works.model";
import { CompanyCategory } from "../../../models/company-page/company-category.model";
import { CompanyWork } from "../../../models/company-page/company-work.model";

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
  work: CompanyWork = {Id: null, Description: null, Name: null, Subcategory: null};  
  isLoaded: boolean = false; 
  openedDetailedWindow: boolean = false;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyWorks(params['id'])).subscribe(res => {
      this.company = res;      
    });
  }

  changeCategory(){    
    console.log(this.selectedCategory);
    this.selectedSubcategory = undefined;
    this.subcategories = this.company.AllCategories.find(x => x.Name == this.selectedCategory.Name).Subcategories;
  }
  selectWorksRow(event: any, work: CompanyWork){   
    if(event.target.localName === "td") {
    this.work = {
      Id: work.Id,
      Description: work.Description,
      Name: work.Name,
      Subcategory: work.Subcategory
    };
    
    this.selectedCategory = work.Subcategory.Category;
    console.log(event.target);
    this.selectedSubcategory = work.Subcategory;
    this.openedDetailedWindow = true;
  }
  else{
    this.deleteWork(work);
  }
  }
  openDetailedWindow(){
    this.work = {
      Id: null,
      Description: null,
      Name: null,
      Subcategory: null
    };
    if(!this.openedDetailedWindow)  
    this.openedDetailedWindow = !this.openedDetailedWindow;
  }
  deleteWork(work: CompanyWork){    
    if(this.work !== null) 
    this.company.Works =  this.company.Works.filter(x => x.Id !== work.Id);   
    this.work = null;  
    if(this.openedDetailedWindow)  
      this.openedDetailedWindow = !this.openedDetailedWindow;
  }
  saveWorkChanges(){    
    if(this.selectedCategory !== undefined && this.selectedSubcategory !== undefined
      && this.work.Description !== null && this.work.Name !== null){
        this.company.Works.find(x => x.Id === this.work.Id).Description = this.work.Description;
        this.company.Works.find(x => x.Id === this.work.Id).Name = this.work.Name;
        this.company.Works.find(x => x.Id === this.work.Id).Subcategory.Category = this.selectedCategory;
        this.company.Works.find(x => x.Id === this.work.Id).Subcategory = this.selectedSubcategory;
        this.saveCompanyWorks();  
      }
  } 
  addWork(){
    if(this.selectedCategory !== undefined && this.selectedSubcategory !== undefined
      && this.work.Description !== null && this.work.Name !== null){ 
        this.selectedCategory.Subcategories = null;
        this.selectedSubcategory.Category = this.selectedCategory;  
        this.work.Subcategory = this.selectedSubcategory;  
        this.company.Works.push(this.work);
        console.log(this.company.Works);
        this.saveCompanyWorks();
    }
  }

  addOrSaveWork(){
    if(this.work.Id !== null){
      this.saveWorkChanges();      
    }
    else{             
        this.addWork();     
    }
  } 

  saveCompanyWorks(){
    this.isLoaded = true;
    this.companyService.saveCompanyWorks(this.company).then(() => {this.isLoaded = false;});
  }
}
