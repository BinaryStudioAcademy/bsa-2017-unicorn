import { Component, OnInit, ViewChild } from '@angular/core';

import {ToastsManager, Toast} from 'ng2-toastr';
import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal} from 'ng2-semantic-ui';

import { OfferService } from '../../../services/offer.service';
import { TokenHelperService } from '../../../services/helper/tokenhelper.service';

import { Offer, OfferStatus } from '../../../models/offer/offer.model';
import { ShortOffer } from '../../../models/offer/shortoffer.model';

export interface IDeclineConfirm {
  id: number;
}

@Component({
  selector: 'app-dashboard-offers',
  templateUrl: './dashboard-offers.component.html',
  styleUrls: ['./dashboard-offers.component.sass']
})
export class DashboardOffersComponent implements OnInit {

  @ViewChild('declineModal')
  public modalTemplate:ModalTemplate<IDeclineConfirm, void, void>
  currModal: SuiActiveModal<IDeclineConfirm, {}, void>;

  offers: Offer[];

  aloads: {[bookId: number]: boolean} = {};
  dloads: {[bookId: number]: boolean} = {};

  reason: string;

  constructor(
    private offerService: OfferService,
    private tokenHelper: TokenHelperService,
    private toastr: ToastsManager,
    private modalService: SuiModalService
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
    this.reason = '';
    const config = new TemplateModalConfig<IDeclineConfirm, void, void>(this.modalTemplate);
    config.context = {id: offer.Id};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.currModal = this.modalService.open(config);
  }

  declineOfferConfirm(id: number) {
    this.currModal.deny(undefined);
    let offer = this.offers.filter(o => o.Id === id)[0];
    offer.Status = OfferStatus.Declined;
    offer.DeclinedMessage = this.reason;
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
