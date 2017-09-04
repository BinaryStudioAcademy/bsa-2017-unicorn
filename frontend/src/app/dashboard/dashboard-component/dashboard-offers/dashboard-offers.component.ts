import { Component, OnInit } from '@angular/core';

import {ToastsManager, Toast} from 'ng2-toastr';

import { OfferService } from '../../../services/offer.service';
import { TokenHelperService } from '../../../services/helper/tokenhelper.service';

import { Offer, OfferStatus } from '../../../models/offer/offer.model';
import { ShortOffer } from '../../../models/offer/shortoffer.model';

@Component({
  selector: 'app-dashboard-offers',
  templateUrl: './dashboard-offers.component.html',
  styleUrls: ['./dashboard-offers.component.sass']
})
export class DashboardOffersComponent implements OnInit {

  offers: Offer[];

  aloads: {[bookId: number]: boolean} = {};
  dloads: {[bookId: number]: boolean} = {};

  constructor(
    private offerService: OfferService,
    private tokenHelper: TokenHelperService,
    private toastr: ToastsManager
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.offerService.getVendorOffers(+this.tokenHelper.getClaimByName('profileid')).then(resp => {
      this.offers = resp;
    });
  }

  acceptOffer(offer: Offer) {
    offer.Status = OfferStatus.Accepted;
    this.aloads[offer.Id] = true;
    this.offerService.updateOffer(offer).then(resp => {
      this.aloads[offer.Id] = false;
      this.offers.splice(this.offers.findIndex(o => o.Id === offer.Id), 1);
      this.toastr.success('Offer successfully accepted', 'Success!');
    }).catch(err => {
      this.aloads[offer.Id] = false;
      this.toastr.error('Something goes wrong', 'Error!');
    });
  }

  declineOffer(offer: Offer) {
    offer.Status = OfferStatus.Declined;
    this.dloads[offer.Id] = true;
    this.offerService.updateOffer(offer).then(resp => {
      this.dloads[offer.Id] = false;
      this.offers.splice(this.offers.findIndex(o => o.Id === offer.Id), 1);
      this.toastr.success('Offer successfully declined', 'Success!');
    }).catch(err => {
      this.dloads[offer.Id] = false;
      this.toastr.error('Something goes wrong', 'Error!');
    });
  }

}
