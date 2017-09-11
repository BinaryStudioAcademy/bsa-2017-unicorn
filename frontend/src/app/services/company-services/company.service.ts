import { Injectable } from '@angular/core';
import { DataService } from "../data.service";
import { CompanyShort } from "../../models/company-page/company-short.model";
import { CompanyDetails } from "../../models/company-page/company-details.model";
import { CompanyReviews } from "../../models/company-page/company-reviews.model";
import { CompanyVendors } from "../../models/company-page/company-vendors.model";
import { CompanyContacts } from "../../models/company-page/company-contacts.model";
import { CompanyWorks } from "../../models/company-page/company-works.model";
import { CompanyBooks } from "../../models/company-page/company-books.model";
import { Contact } from "../../models/contact.model";
import { Vendor } from "../../register/models/vendor";
import { CompanyWork } from "../../models/company-page/company-work.model";
import { CompanyBook } from "../../models/company-page/company-book.model";
import { Router } from "@angular/router";

@Injectable()
export class CompanyService { 
  
  constructor(private dataService: DataService, private router: Router) {
    dataService.setHeader('Content-Type', 'application/json');
   }

  getCompanies():Promise<CompanyShort[]>{
    return this.dataService.getRequest<CompanyShort[]>("company");      
  }



  getCompanyShort(id: number):Promise<any>{
    return this.dataService.getRequest<CompanyShort>("company/short/" + id)
    .catch(err => this.router.navigate([`not-found`], {
      queryParams: {
        message: `this company doesn’t exist. Try to find someone else.`,
      }}))
  }
  getCompanyDetails(id: number):Promise<any>{
    return this.dataService.getRequest<CompanyDetails>("company/details/" + id)
    .catch(err => location.href = 'index'); 
  }
  saveCompanyDetails(companyDetails: CompanyDetails):Promise<any>{
    return this.dataService.postRequest("company/details", companyDetails)
    .catch(err => location.href = 'index');
  }



  getCompanyReviews(id: number):Promise<any>{
    return this.dataService.getRequest<CompanyReviews>("company/reviews/" + id)
    .catch(err => location.href = 'index');    
  }
  addCompanyReviews(companyReviews: CompanyReviews):Promise<any>{
    return this.dataService.putRequest("company/reviews", companyReviews)
    .catch(err => location.href = 'index');
  }

  

  getCompanyVendors(id: number):Promise<any>{
    return this.dataService.getRequest<CompanyVendors>("company/vendors/" + id)
    .catch(err => location.href = 'index');    
  }
  addCompanyVendors(companyVendors: CompanyVendors):Promise<any>{
    return this.dataService.putRequest("company/vendors", companyVendors)
    .catch(err => location.href = 'index');
  }
  deleteCompanyVendor(companyId: number, vendorId: number):Promise<any>{
    return this.dataService.deleteRequest("company/vendor/"+ companyId + "/" + vendorId)
    .catch(err => location.href = 'index');
  }



  getCompanyContacts(id: number):Promise<any>{
    return this.dataService.getRequest<CompanyContacts>("company/contacts/" + id)
    .catch(err => location.href = 'index');    
  }
  saveCompanyContact(companyContact: Contact):Promise<any>{
    return this.dataService.postRequest("company/contact", companyContact)
    .catch(err => location.href = 'index');
  }
  addCompanyContact(companyId: number, companyContact: Contact):Promise<any>{
    return this.dataService.putRequest("company/contact/" + companyId, companyContact)
    .catch(err => location.href = 'index');
  }
  deleteCompanyContact(companyId: number, contactId: number):Promise<any>{
    return this.dataService.deleteRequest("company/contact/"+ companyId + "/" + contactId)
    .catch(err => location.href = 'index');
  }



  getCompanyWorks(id: number):Promise<any>{
    return this.dataService.getRequest<CompanyWorks>("company/works/" + id)
    .catch(err => location.href = 'index');    
  }
  saveCompanyWork(companyWork: CompanyWork):Promise<any>{
    return this.dataService.postRequest("company/work", companyWork)
    .catch(err => location.href = 'index');
  }
  addCompanyWork(companyId: number, companyWork: CompanyWork):Promise<any>{
    return this.dataService.putRequest("company/work/" + companyId, companyWork)
    .catch(err => location.href = 'index');
  }
  deleteCompanyWork(companyId: number, workId: number):Promise<any>{
    return this.dataService.deleteRequest("company/work/"+ companyId + "/" + workId)
    .catch(err => location.href = 'index');
  }



  getCompanyBooks(id: number):Promise<any>{
    return this.dataService.getRequest<CompanyBooks>("company/books/" + id)
    .catch(err => location.href = 'index');    
  } 
  saveCompanyBook(companyBook: CompanyBook):Promise<any>{
    return this.dataService.postRequest("company/book", companyBook)
    .catch(err => location.href = 'index');
  }  



  getCompanyRating(id: number):Promise<any>{
    return this.dataService.getFullRequest<number>("company/" + id + "/rating")
    .catch(err => location.href = 'index');    
  }
}