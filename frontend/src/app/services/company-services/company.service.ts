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

@Injectable()
export class CompanyService { 
  
  constructor(private dataService: DataService) {
    dataService.setHeader('Content-Type', 'application/json');
   }

  getCompanies():Promise<CompanyShort[]>{
    return this.dataService.getRequest<CompanyShort[]>("company");      
  }



  getCompanyShort(id: number):Promise<CompanyShort>{
    return this.dataService.getRequest<CompanyShort>("company/short/" + id);    
  }
  getCompanyDetails(id: number):Promise<CompanyDetails>{
    return this.dataService.getRequest<CompanyDetails>("company/details/" + id);    
  }
  saveCompanyDetails(companyDetails: CompanyDetails):Promise<CompanyDetails>{
    return this.dataService.postRequest("company/details", companyDetails);
  }



  getCompanyReviews(id: number):Promise<CompanyReviews>{
    return this.dataService.getRequest<CompanyReviews>("company/reviews/" + id);    
  }
  addCompanyReviews(companyReviews: CompanyReviews):Promise<CompanyReviews>{
    return this.dataService.putRequest("company/reviews", companyReviews);
  }

  

  getCompanyVendors(id: number):Promise<CompanyVendors>{
    return this.dataService.getRequest<CompanyVendors>("company/vendors/" + id);    
  }
  addCompanyVendors(companyVendors: CompanyVendors):Promise<CompanyVendors>{
    return this.dataService.putRequest("company/vendors", companyVendors);
  }
  deleteCompanyVendor(companyId: number, vendorId: number):Promise<Vendor>{
    return this.dataService.deleteRequest("company/vendor/"+ companyId + "/" + vendorId, {});
  }



  getCompanyContacts(id: number):Promise<CompanyContacts>{
    return this.dataService.getRequest<CompanyContacts>("company/contacts/" + id);    
  }
  saveCompanyContact(companyContact: Contact):Promise<Contact>{
    return this.dataService.postRequest("company/contact", companyContact);
  }
  addCompanyContact(companyId: number, companyContact: Contact):Promise<Contact>{
    return this.dataService.putRequest("company/contact/" + companyId, companyContact);
  }
  deleteCompanyContact(companyId: number, contactId: number):Promise<Contact>{
    return this.dataService.deleteRequest("company/contact/"+ companyId + "/" + contactId, {});
  }



  getCompanyWorks(id: number):Promise<CompanyWorks>{
    return this.dataService.getRequest<CompanyWorks>("company/works/" + id);    
  }
  saveCompanyWork(companyWork: CompanyWork):Promise<CompanyWork>{
    return this.dataService.postRequest("company/work", companyWork);
  }
  addCompanyWork(companyId: number, companyWork: CompanyWork):Promise<CompanyWork>{
    return this.dataService.putRequest("company/work/" + companyId, companyWork);
  }
  deleteCompanyWork(companyId: number, workId: number):Promise<CompanyWork>{
    return this.dataService.deleteRequest("company/work/"+ companyId + "/" + workId, {});
  }



  getCompanyBooks(id: number):Promise<CompanyBooks>{
    return this.dataService.getRequest<CompanyBooks>("company/books/" + id);    
  } 
  saveCompanyBook(companyBook: CompanyBook):Promise<CompanyBook>{
    return this.dataService.postRequest("company/book", companyBook);
  }  



  getCompanyRating(id: number):Promise<any>{
    return this.dataService.getFullRequest<number>("company/" + id + "/rating");    
  }
  getSearchCompanies(category: string, subcategory: string, date: number): Promise<CompanyDetails[]> {
    const query = `company-search?category={category}&subcategory={subcategory}&date={date}`;
    return this.dataService.getRequest<CompanyDetails[]>(query);
  }
}