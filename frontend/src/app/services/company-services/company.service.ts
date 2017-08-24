import { Injectable } from '@angular/core';
import { DataService } from "../data.service";
import { CompanyShort } from "../../models/company-page/company-short.model";
import { CompanyDetails } from "../../models/company-page/company-details.model";
import { CompanyReviews } from "../../models/company-page/company-reviews.model";
import { CompanyVendors } from "../../models/company-page/company-vendors.model";
import { CompanyContacts } from "../../models/company-page/company-contacts.model";
import { CompanyWorks } from "../../models/company-page/company-works.model";
import { CompanyBooks } from "../../models/company-page/company-books.model";

@Injectable()
export class CompanyService { 
  
  constructor(private dataService: DataService) {
    dataService.setHeader('Content-Type', 'application/json');
   }

  getCompanies():Promise<CompanyShort[]>{
    return this.dataService.getRequest<CompanyShort[]>("company");      
  }

  getCompanyShort(id: number):Promise<CompanyShort>{
    return this.dataService.getRequest<CompanyShort>("company-short/" + id);    
  }

  getCompanyDetails(id: number):Promise<CompanyDetails>{
    return this.dataService.getRequest<CompanyDetails>("company-details/" + id);    
  }

  getCompanyReviews(id: number):Promise<CompanyReviews>{
    return this.dataService.getRequest<CompanyReviews>("company-reviews/" + id);    
  }
  getCompanyRating(id: number):Promise<any>{
    return this.dataService.getFullRequest<number>("company/" + id + "/rating");    
  }

  getCompanyVendors(id: number):Promise<CompanyVendors>{
    return this.dataService.getRequest<CompanyVendors>("company-vendors/" + id);    
  }

  getCompanyContacts(id: number):Promise<CompanyContacts>{
    return this.dataService.getRequest<CompanyContacts>("company-contacts/" + id);    
  }

  getCompanyWorks(id: number):Promise<CompanyWorks>{
    return this.dataService.getRequest<CompanyWorks>("company-works/" + id);    
  }

  getCompanyBooks(id: number):Promise<CompanyBooks>{
    return this.dataService.getRequest<CompanyBooks>("company-books/" + id);    
  }

  saveCompanyDetails(company: CompanyDetails):Promise<CompanyDetails>{
    return this.dataService.postRequest("company-details", company);
  }

  saveCompanyReviews(company: CompanyReviews):Promise<CompanyReviews>{
    return this.dataService.postRequest("company-reviews", company);
  }

  saveCompanyVendors(company: CompanyVendors):Promise<CompanyVendors>{
    return this.dataService.postRequest("company-vendors", company);
  }

  saveCompanyContacts(company: CompanyContacts):Promise<CompanyContacts>{
    return this.dataService.postRequest("company-contacts", company);
  }

  saveCompanyWorks(company: CompanyWorks):Promise<CompanyWorks>{
    return this.dataService.postRequest("company-works", company);
  }

  saveCompanyBooks(company: CompanyBooks):Promise<CompanyBooks>{
    return this.dataService.postRequest("company-books", company);
  }

  getMockCompany(): CompanyShort{
    return { 
      Id: 1,
      Avatar: "../../../assets/images/company_logo.png",
      Name: "TURBOCAT 9000 Inc.",      
      Location: {
        Id: 1,
        PostIndex: "74",
        Adress: "Chornovola 2",
        City: "Lviv",
        Latitude: Math.random() * 0.0099 + 43.7250,
        Longitude: Math.random() * 0.0099 + -79.7699,
      }
    };
  }

}
