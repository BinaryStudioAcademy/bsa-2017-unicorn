import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model';
import { PortfolioItem } from '../models/portfolio-item.model';
import { Rating } from "../models/rating.model";
import { Review } from "../models/review.model";
import { Subcategory } from "../models/subcategory.model";
import { Contact } from "../models/contact.model";
import { Category } from "../models/category.model";
import { VendorBook } from "../models/book/vendor-book.model";

@Injectable()
export class VendorService {
  private apiController: string;

  constructor(private dataService: DataService) 
  { 
    dataService.setHeader('Content-Type', 'application/json');
    this.apiController = "vendors";
  }

  getAllVendors() : Promise<Vendor[]> {
    return this.dataService.getFullRequest<Vendor[]>(this.apiController);
  }

  getVendor(id: number) : Promise<any> {
    return this.dataService.getFullRequest<Vendor>(`${this.apiController}/${id}`)
      .catch(err => alert(err));
  }

  getRating(id: number): Promise<any> {
    return this.dataService.getFullRequest<Rating>(`${this.apiController}/${id}/rating`)
      .catch(err => alert(err));
  }

  getCategories(id: number): Promise<any> {
    return this.dataService.getFullRequest<Category[]>(`${this.apiController}/${id}/categories`)
      .catch(err => alert(err));
  }

  getOrders(id: number): Promise<any> {
    return this.dataService.getFullRequest<VendorBook>(`${this.apiController}/${id}/orders`)
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

  updateVendor(vendor: Vendor): Promise<any> {
    return this.dataService.putFullRequest<Vendor>(`${this.apiController}/${vendor.Id}`, vendor)
      .catch(err => alert(err));
  }

  updateVendorPortfolio(vendorId: number, portfolio: PortfolioItem[]): Promise<any> {
    return this.dataService.putFullRequest<PortfolioItem[]>(`${this.apiController}/${vendorId}/portfolio`, portfolio)
      .catch(err => alert(err));
  }

  updateContacts(id: number, contacts: Contact[]): Promise<any> {
    return this.dataService.putFullRequest<Contact[]>(`${this.apiController}/${id}/contacts`, contacts)
      .catch(err => alert(err));;
  }
}
