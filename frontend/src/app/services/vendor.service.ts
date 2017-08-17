import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model';
import { PortfolioItem } from '../models/portfolio-item.model';
import { Rating } from "../models/rating.model";
import { Review } from "../models/review.model";
import { Subcategory } from "../models/subcategory.model";
import { Contact } from "../models/contact.model";

@Injectable()
export class VendorService {
  private apiController: string;

  constructor(private dataService: DataService) 
  { 
    this.apiController = "vendors";
  }

  getAllVendors() : Promise<Vendor[]> {
    return this.dataService.getFullRequest<Vendor[]>(this.apiController);
  }

  getVendor(id: number) : Promise<any> {
    return this.dataService.getFullRequest<Vendor>(`${this.apiController}/${id}`);
  }

  getRating(id: number): Promise<any> {
    return this.dataService.getFullRequest<Rating>(`${this.apiController}/${id}/rating`);
  }

  getSubcategories(id: number): Promise<any> {
    return this.dataService.getFullRequest<Subcategory[]>(`${this.apiController}/${id}/categories`);
  }

  getReviews(id: number): Promise<any> {
    return this.dataService.getFullRequest<Review[]>(`${this.apiController}/${id}/reviews`);
  }

  getContacts(id: number): Promise<any> {
    return this.dataService.getFullRequest<Contact[]>(`${this.apiController}/${id}/contacts`);
  }

  getVendorPorfolio(vendorId: number): Promise<any> {
    return this.dataService.getFullRequest<PortfolioItem[]>(`${this.apiController}/${vendorId}/portfolio`);
  }
}
