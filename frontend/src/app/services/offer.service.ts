import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Offer } from '../models/offer/offer.model';
import { ShortOffer } from '../models/offer/shortoffer.model';

@Injectable()
export class OfferService {

  constructor(
    private dataService: DataService
  ) { 
    this.dataService.setHeader('Content-Type', 'application/json');
  }

  createOffers(offers: ShortOffer[]): Promise<any> {
    return this.dataService.postRequest('offer', offers);
  }

  getVendorOffers(id: number): Promise<Offer[]> {
    return this.dataService.getRequest(`offer/vendor/${id}`);
  }

  getCompanyOffers(id: number): Promise<Offer[]> {
    return this.dataService.getRequest(`offer/company/${id}`);
  }

  updateOffer(offer: Offer): Promise<any> {
    return this.dataService.putRequest('offer', offer);
  }

  deleteOffer(id: number): Promise<any> {
    return this.dataService.deleteRequest(`offer/${id}`);
  }

}
