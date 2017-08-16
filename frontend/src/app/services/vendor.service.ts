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
  private resourceUrl: string;

  constructor(private dataService: DataService) 
  { 
    this.resourceUrl = dataService.buildUrl("vendors");
  }

  getAllVendors() : Promise<Vendor[]> {
    return this.dataService.getRequest<Vendor[]>(this.resourceUrl);
  }

  getVendor(id: number) : Promise<Vendor> {
    return this.dataService.getRequest<Vendor>(`${this.resourceUrl}/${id}`);
  }

  getRating(id: number): Promise<Rating> {
    return this.dataService.getRequest<Rating>(`${this.resourceUrl}/${id}/rating`);
  }

  getSubcategories(id: number): Promise<Subcategory[]> {
    return this.dataService.getRequest<Subcategory[]>(`${this.resourceUrl}/${id}/categories`);
  }

  getReviews(id: number): Promise<Review[]> {
    return this.dataService.getRequest<Review[]>(`${this.resourceUrl}/${id}/reviews`);
  }

  getContacts(id: number): Promise<Contact[]> {
    return this.dataService.getRequest<Contact[]>(`${this.resourceUrl}/${id}/contacts`);
  }

  getVendorPorfolio(vendorId: number): Promise<PortfolioItem[]> {
    return this.dataService.getRequest<PortfolioItem[]>(`${this.resourceUrl}/${vendorId}/portfolio`);
  }
}
