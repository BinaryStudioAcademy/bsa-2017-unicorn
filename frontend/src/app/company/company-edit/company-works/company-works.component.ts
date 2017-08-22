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
  selectedDisabled: boolean = false;
  isLoaded: boolean = false;
  saveDisabled: boolean = true;
  addEnable: boolean = false;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyWorks(params['id'])).subscribe(res => {
      this.company = res;      
    });
  }

  changeCategory(){    
    this.selectedSubcategory = undefined;
    this.subcategories = this.company.AllCategories.find(x => x.Name == this.selectedCategory.Name).Subcategories;
  }

  selectWorksRow(work: CompanyWork){    
    this.work = {
      Id: work.Id,
      Description: work.Description,
      Name: work.Name,
      Subcategory: work.Subcategory
    };

    this.selectedDisabled = true;
  }

  deleteWork(work: CompanyWork){    
    if(this.work !== null) 
    this.company.Works =  this.company.Works.filter(x => x.Id !== work.Id);   

    this.selectedDisabled = false;
  }

  saveWorkChanges(){    
      this.company.Works.find(x => x.Id === this.work.Id).Description = this.work.Description;
      this.company.Works.find(x => x.Id === this.work.Id).Name = this.work.Name;
      this.saveCompanyWorks();      
      this.selectedDisabled = false;      
  } 

  addWork(){
    this.selectedCategory.Subcategories = null;
    this.selectedSubcategory.Category = this.selectedCategory;  
    this.work.Subcategory = this.selectedSubcategory;  
    this.company.Works.push(this.work);
    console.log(this.company.Works);
    this.saveCompanyWorks();
  }

  addOrSaveWork(){
    //console.log(this.selectedCategory, this.selectedSubcategory);
    if(this.selectedDisabled){
      this.saveWorkChanges();      
    }
    else{
      if(this.selectedCategory !== undefined && this.selectedSubcategory !== undefined
      && this.work.Description !== null && this.work.Name !== null){        
        this.addWork();
      }
    }
  }


  saveCompanyWorks(){
    this.isLoaded = true;
    this.companyService.saveCompanyWorks(this.company).then(() => {this.isLoaded = false;});
  }
}
