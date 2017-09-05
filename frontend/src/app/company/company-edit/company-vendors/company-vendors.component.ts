import { Component, OnInit, ViewChild, NgZone } from '@angular/core';
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyService } from "../../../services/company-services/company.service";
import { OfferService } from "../../../services/offer.service";
import { CompanyVendors } from "../../../models/company-page/company-vendors.model";
import { Offer, OfferStatus } from '../../../models/offer/offer.model';
import { ShortOffer } from '../../../models/offer/shortoffer.model';
import { Vendor } from "../../../models/company-page/vendor";
import { SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import {ToastsManager, Toast} from 'ng2-toastr';

export interface IMessageContext {
  id: number;
}

export interface IDeclinedReasonContext {
  reason: string;
}

@Component({
  selector: 'app-company-vendors',
  templateUrl: './company-vendors.component.html',
  styleUrls: ['./company-vendors.component.sass']
})
export class CompanyVendorsComponent implements OnInit { 
  @ViewChild('modalDeleteTemplate')
  public modalDeleteTemplate: ModalTemplate<void, {}, void>;

  private activeModal: SuiActiveModal<void, {}, void>;
  private messModal: SuiActiveModal<IMessageContext, {}, void>;

  @ViewChild('messageModal')
  public modalTemplate:ModalTemplate<IMessageContext, void, void>;
  @ViewChild('reasonModal')
  public reasonModal:ModalTemplate<IDeclinedReasonContext, void, void>

  company: CompanyVendors;  
  companyId: number;   
  isLoaded: boolean = false; 
  openedDetailedWindow: boolean = false;  
  allVendors: Vendor[];
  selectedVendors: Vendor[] = [];
  selectedVendor: Vendor;
  vendor: Vendor;

  pendingVendors: Offer[] = [];

  messages: {[bookId: number]: string} = {};

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private zone: NgZone,
    private suiModalService: SuiModalService,
    private toastr: ToastsManager,
    private offerService: OfferService
  ) { }

  ngOnInit() { 
    this.initializeThisCompany();   
  }  

  initializeThisCompany(){
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyVendors(params['id'])).subscribe(res => {
      this.company = res;   
      this.allVendors = this.company.AllVendors;
      this.companyId = this.company.Id;  
      this.offerService.getCompanyOffers(this.route.snapshot.params['id']).then(resp => {
        this.pendingVendors = resp.filter(o => o.Status !== OfferStatus.Accepted);
        console.log('pend', resp);
        this.pendingVendors.forEach(p => {
          this.allVendors = this.allVendors.filter(x => x.Id !== p.Vendor.Id);
        });
      });
    });
    
  }

  saveMessage() {
    console.log(this.messages);
    this.messModal.deny(undefined);
  }

  openMessageModal(vendor: Vendor) {
    const config = new TemplateModalConfig<IMessageContext, void, void>(this.modalTemplate);
    config.context = {id: vendor.Id};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.messModal = this.suiModalService.open(config);
  }

  changeVendor(){
    this.selectedVendors.push(this.selectedVendor);
    this.allVendors = this.allVendors.filter(x => x.Id !== this.selectedVendor.Id);
    this.pendingVendors.forEach(p => {
      this.allVendors = this.allVendors.filter(x => x.Id !== p.Vendor.Id);
    });
    this.zone.run(() => { this.selectedVendor = null; });  
  }

  openDetailedWindow(){
    if(!this.openedDetailedWindow)  {
      this.openedDetailedWindow = true;  
      this.selectedVendors = [];
    }    
  }

  closeDetailedWindow(){
    this.openedDetailedWindow = false;  
    this.allVendors = this.company.AllVendors;
    this.pendingVendors.forEach(p => {
      this.allVendors = this.allVendors.filter(x => x.Id !== p.Vendor.Id);
    });
    this.selectedVendors = undefined;
    this.zone.run(() => { this.selectedVendor = null; });  
  }

  deleteVendor(){ 
    if(this.activeModal !== undefined){ 
    this.activeModal.deny(null);  
    } 
    if(this.openedDetailedWindow){
      this.openedDetailedWindow = false; 
    }    
    this.selectedVendors = undefined;
    this.zone.run(() => { this.selectedVendor = null; }); 

    this.company = undefined;  
    this.companyService.deleteCompanyVendor(this.companyId, this.vendor.Id)
      .then(() => {
        this.initializeThisCompany();
        this.toastr.success('Changes were saved', 'Success!');   
      }).catch(err => this.toastr.error('Something goes wrong', 'Error!'));     
  }

  deleteSelectedVendor(vendor: Vendor){
    this.selectedVendors = this.selectedVendors.filter(x => x.Id !== vendor.Id);
    this.zone.run(() => { this.selectedVendor = null; });
    this.allVendors.push(vendor);
  }

  addVendors(){      
    if(this.selectedVendors.length !== 0){
      this.selectedVendors.forEach(vendor => {this.company.Vendors.push(vendor);});      
      //this.saveCompanyVendors(); 
      this.offerVendors(); 
      this.selectedVendors = undefined;
      this.zone.run(() => { this.selectedVendor = null; });  
      this.company = undefined;    
      this.openedDetailedWindow = false;     
    }
  }

  saveCompanyVendors(){
    this.isLoaded = true;    
    this.companyService.addCompanyVendors(this.company).then(() => {
      this.isLoaded = false;      
      this.initializeThisCompany();
      this.toastr.success('Changes were saved', 'Success!');
    }).catch(err => this.toastr.error('Something goes wrong', 'Error!'));
  }

  offerVendors() {
    let offers: ShortOffer[] = [];
    this.selectedVendors.forEach(vendor => {
      let offer: ShortOffer = {
        AttachedMessage: this.messages[vendor.Id],
        CompanyId: this.companyId,
        VendorId: vendor.Id
      }
      offers.push(offer);
    });
    this.offerService.createOffers(offers).then(resp => {
      this.initializeThisCompany();
      this.toastr.success('Offers were sended', 'Success!');
    }).catch(err => {
      this.toastr.error('Something goes wrong', 'Error!');
    });
    
  }

  deleteOffer(offer: Offer) {
    this.offerService.deleteOffer(offer.Id).then(resp => {
      this.initializeThisCompany();
      this.toastr.success('Offer was deleted', 'Success!');
    }).catch(err => {
      this.toastr.error('Something goes wrong', 'Error!');
    });
    this.selectedVendors = undefined;
    this.zone.run(() => { this.selectedVendor = null; });  
    this.company = undefined;    
    this.openedDetailedWindow = false;
  }

  haveReason(offer: Offer): boolean {
    return offer.DeclinedMessage !== undefined && offer.DeclinedMessage !== null && offer.DeclinedMessage !== '';
  }

  showReason(reason: string) {
    const config = new TemplateModalConfig<IDeclinedReasonContext, void, void>(this.reasonModal);
    config.context = {reason: reason};
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.suiModalService.open(config);
  }

  getOfferStatus(offer: Offer): string {
    switch (offer.Status) {
      case OfferStatus.Pending: return 'Pending';
      case OfferStatus.Declined: return 'Declined';
      case OfferStatus.Accepted: return 'Accepted';
    }
    return '';
  }

  openDeleteModal(vendor: Vendor){
    this.vendor = vendor;
    this.activeModal = this.openDelModal(this.modalDeleteTemplate);
  }

  public openDelModal(modalTemplate: ModalTemplate<void, {}, void>): SuiActiveModal<void, {}, void> {
    const config = new TemplateModalConfig<void, {}, void>(modalTemplate);
    //config.closeResult = "closed!";

    config.size = ModalSize.Mini;
    config.isInverted = true;
    //config.mustScroll = true;
    let that = this;

    return this.suiModalService
      .open(config)
      .onApprove(result => { /* approve callback */ })
      .onDeny(result => {  /* deny callback */   });
  }
}