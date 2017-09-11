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
import { Router } from "@angular/router";

@Injectable()
export class VendorService {
  private apiController: string;

  constructor(private dataService: DataService, private router: Router) 
  { 
    dataService.setHeader('Content-Type', 'application/json');
    this.apiController = "vendors";
  }

  getAllVendors() : Promise<any> {
    return this.dataService.getFullRequest<Vendor[]>(this.apiController)
    .catch(err => location.href = 'index');
  }

  getVendor(id: number) : Promise<any> {
    return this.dataService.getFullRequest<Vendor>(`${this.apiController}/${id}`)
    .catch(err => this.router.navigate([`not-found`], {
      queryParams: {
        message: `this vendor doesn’t exist. Try to find someone else.`,
      }}))
  }

  getRating(id: number): Promise<any> {
    return this.dataService.getFullRequest<number>(`${this.apiController}/${id}/rating`)
    .catch(err => location.href = 'index');
   }

  getCategories(id: number): Promise<any> {
    return this.dataService.getFullRequest<Category[]>(`${this.apiController}/${id}/categories`)
    .catch(err => location.href = 'index');
  }

  getVendorWorks(id: number): Promise<any> {
    return this.dataService.getFullRequest<Work[]>(`${this.apiController}/${id}/works`)
    .catch(err => location.href = 'index');
  }

  getOrders(id: number): Promise<any> {
    return this.dataService.getFullRequest<VendorBook[]>(`${this.apiController}/${id}/orders`)
    .catch(err => location.href = 'index');
  }

  getReviews(id: number): Promise<any> {
    return this.dataService.getFullRequest<Review[]>(`${this.apiController}/${id}/reviews`)
    .catch(err => location.href = 'index');
  }

  getContacts(id: number): Promise<any> {
    return this.dataService.getFullRequest<Contact[]>(`${this.apiController}/${id}/contacts`)
    .catch(err => location.href = 'index');
  }

  getVendorPorfolio(vendorId: number): Promise<any> {
    return this.dataService.getFullRequest<PortfolioItem[]>(`${this.apiController}/${vendorId}/portfolio`)
    .catch(err => location.href = 'index');
  }

  getVendorHistory(vendorId: number): Promise<any> {
    return this.dataService.getFullRequest<VendorHistory[]>(`${this.apiController}/${vendorId}/history`)
    .catch(err => location.href = 'index');
  }

  postVendorWork(vendorId: number, work: Work): Promise<any> {
    return this.dataService.postFullRequest<Work>(`${this.apiController}/${vendorId}/works`, work)
    .catch(err => location.href = 'index');
  }

  postVendorPorfolio(vendorId: number, item: PortfolioItem): Promise<any> {
    return this.dataService.postFullRequest<PortfolioItem>(`${this.apiController}/${vendorId}/portfolio`, item)
    .catch(err => location.href = 'index');
  }

  postVendorContact(vendorId: number, contact: Contact): Promise<any> {
    return this.dataService.postFullRequest<Contact>(`${this.apiController}/${vendorId}/contacts`, contact)
    .catch(err => location.href = 'index');
  }

  updateVendor(vendor: Vendor): Promise<any> {
    return this.dataService.putFullRequest<Vendor>(`${this.apiController}/${vendor.Id}`, vendor)
    .catch(err => location.href = 'index');
  }

  updateVendorPortfolio(vendorId: number, portfolio: PortfolioItem[]): Promise<any> {
    return this.dataService.putFullRequest<PortfolioItem[]>(`${this.apiController}/${vendorId}/portfolio`, portfolio)
    .catch(err => location.href = 'index');
  }

  updateContact(id: number, contact: Contact): Promise<any> {
    return this.dataService.putFullRequest<Contact>(`${this.apiController}/${id}/contacts/${contact.Id}`, contact)
    .catch(err => location.href = 'index');
  }

  updateOrder(id: number, order: VendorBook): Promise<any> {
    return this.dataService.putFullRequest<VendorBook[]>(`${this.apiController}/${id}/orders/${order.Id}`, order)
    .catch(err => location.href = 'index');
  }

  updateVendorWork(vendorId: number, workId: number, work: Work): Promise<any> {
    return this.dataService.putFullRequest<Work>(`${this.apiController}/${vendorId}/works/${workId}`, work)
    .catch(err => location.href = 'index');
  }

  removeVendorWork(vendorId: number, workId: number, work: Work): Promise<any> {
    return this.dataService.deleteFullRequest<Work[]>(`${this.apiController}/${vendorId}/works/${workId}`, work)
    .catch(err => location.href = 'index');
  }

  removeContact(id: number, contact: Contact): Promise<any> {
    return this.dataService.deleteFullRequest<any>(`${this.apiController}/${id}/contacts/${contact.Id}`, contact)
    .catch(err => location.href = 'index');
  }
}
