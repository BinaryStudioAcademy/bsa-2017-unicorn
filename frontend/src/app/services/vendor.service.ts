import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model';
import { PortfolioItem } from '../models/portfolio-item.model';
import { Rating } from "../models/rating.model";

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
    return this.dataService.getRequest<any>(`${this.resourceUrl}/${id}`);
  }

  getRating(id: number): Promise<Rating> {
    return this.dataService.getRequest<any>(`${this.resourceUrl}/${id}/rating`);
  }

  getVendorPorfolio(vendorId: number): Promise<PortfolioItem[]> {
    return this.dataService.getRequest<any>(`${this.resourceUrl}/${vendorId}/portfolio`);
  }
}
