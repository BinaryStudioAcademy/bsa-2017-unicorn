import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model';
import { PortfolioItem } from '../models/portfolio-item.model';
import { Review } from "../models/review.model";
import { Subcategory } from "../models/subcategory.model";
import { Contact } from "../models/contact.model";
import { Category } from "../models/category.model";
import { VendorBook } from "../models/book/vendor-book.model";
import { VendorHistory } from "../models/vendor-history.model";
import { Work } from "../models/work.model";

@Injectable()
export class VendorService {
  private apiController: string;

  constructor(private dataService: DataService) 
  { 
    dataService.setHeader('Content-Type', 'application/json');
    this.apiController = "vendors";
  }

  getAllVendors() : Promise<any> {
    return this.dataService.getFullRequest<Vendor[]>(this.apiController)
      .catch(err => alert(err));
  }

  getVendor(id: number) : Promise<any> {
    return this.dataService.getFullRequest<Vendor>(`${this.apiController}/${id}`)
      .catch(err => alert(err));
  }

  getRating(id: number): Promise<any> {
    return this.dataService.getFullRequest<number>(`${this.apiController}/${id}/rating`)
      .catch(err => alert(err));
   }

  getCategories(id: number): Promise<any> {
    return this.dataService.getFullRequest<Category[]>(`${this.apiController}/${id}/categories`)
      .catch(err => alert(err));
  }

  getVendorWorks(id: number): Promise<any> {
    return this.dataService.getFullRequest<Work[]>(`${this.apiController}/${id}/works`)
      .catch(err => alert(err));
  }

  getOrders(id: number): Promise<any> {
    return this.dataService.getFullRequest<VendorBook[]>(`${this.apiController}/${id}/orders`)
      .catch(err => alert(err));
  }

  getReviews(id: number): Promise<any> {
    return this.dataService.getFullRequest<Review[]>(`${this.apiController}/${id}/reviews`)
      .catch(err => alert(err));
  }

  getContacts(id: number): Promise<any> {
    return this.dataService.getFullRequest<Contact[]>(`${this.apiController}/${id}/contacts`)
      .catch(err => alert(err));;
  }

  getVendorPorfolio(vendorId: number): Promise<any> {
    return this.dataService.getFullRequest<PortfolioItem[]>(`${this.apiController}/${vendorId}/portfolio`)
      .catch(err => alert(err));
  }

  getVendorHistory(vendorId: number): Promise<any> {
    return this.dataService.getFullRequest<VendorHistory[]>(`${this.apiController}/${vendorId}/history`)
      .catch(err => alert(err));
  }

  postVendorWork(vendorId: number, work: Work): Promise<any> {
    return this.dataService.postFullRequest<Work[]>(`${this.apiController}/${vendorId}/works`, work)
      .catch(err => alert(err));
  }

  postVendorPorfolio(vendorId: number, item: PortfolioItem): Promise<any> {
    return this.dataService.postFullRequest<PortfolioItem>(`${this.apiController}/${vendorId}/portfolio`, item)
      .catch(err => alert(err));
  }

  postVendorContact(vendorId: number, contact: Contact): Promise<any> {
    return this.dataService.postFullRequest<Contact[]>(`${this.apiController}/${vendorId}/contacts`, contact)
      .catch(err => alert(err));
  }

  updateVendor(vendor: Vendor): Promise<any> {
    return this.dataService.putFullRequest<Vendor>(`${this.apiController}/${vendor.Id}`, vendor)
      .catch(err => alert(err));
  }

  updateVendorPortfolio(vendorId: number, portfolio: PortfolioItem[]): Promise<any> {
    return this.dataService.putFullRequest<PortfolioItem[]>(`${this.apiController}/${vendorId}/portfolio`, portfolio)
      .catch(err => alert(err));
  }

  updateContact(id: number, contact: Contact): Promise<any> {
    return this.dataService.putFullRequest<Contact>(`${this.apiController}/${id}/contacts/${contact.Id}`, contact)
      .catch(err => alert(err));;
  }

  updateOrder(id: number, order: VendorBook): Promise<any> {
    return this.dataService.putFullRequest<VendorBook[]>(`${this.apiController}/${id}/orders/${order.Id}`, order)
      .catch(err => alert(err));
  }

  updateVendorWork(vendorId: number, workId: number, work: Work): Promise<any> {
    return this.dataService.putFullRequest<Work>(`${this.apiController}/${vendorId}/works/${workId}`, work)
      .catch(err => alert(err));
  }

  removeVendorWork(vendorId: number, workId: number, work: Work): Promise<any> {
    return this.dataService.deleteFullRequest<Work[]>(`${this.apiController}/${vendorId}/works/${workId}`, work)
      .catch(err => alert(err));
  }

  removeContact(id: number, contact: Contact): Promise<any> {
    return this.dataService.deleteFullRequest<any>(`${this.apiController}/${id}/contacts/${contact.Id}`, contact)
      .catch(err => alert(err));;
  }
}
