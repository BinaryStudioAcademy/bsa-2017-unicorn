import { Component, OnInit, Input, AfterViewChecked, ViewChild } from '@angular/core';

import { NguiMapModule, Marker } from "@ngui/map";

import { MapModel } from "../../../models/map.model";
import { Vendor } from '../../../models/vendor.model';
import { Contact } from "../../../models/contact.model";
import { LocationModel } from "../../../models/location.model";
import { LocationService } from "../../../services/location.service";
import { VendorService } from "../../../services/vendor.service";

import { ChatService } from "../../../services/chat/chat.service";
import { TokenHelperService } from "../../../services/helper/tokenhelper.service";
import { DialogModel } from "../../../models/chat/dialog.model";
import { ChatEventsService } from "../../../services/events/chat-events.service";

@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass'],
})
export class VendorProfileContactsComponent implements OnInit {
  @Input() private vendorId: number;
  @Input() private accountId: number;
  
  contacts: Contact[];
  location: LocationModel;
  map: MapModel;
  vendor: Vendor;

  openChat: boolean = false;
  ownerId: number;
  dialog: DialogModel;
  isLoaded: boolean = false;

  constructor(
    private locationService: LocationService, 
    private vendorService: VendorService,
    private chatService: ChatService,
    private tokenHelper: TokenHelperService,
    private chatEventsService: ChatEventsService
  ) { }

  ngOnInit() {
    
    this.vendorService.getVendor(this.vendorId)
    .then(resp => 
      { 
        this.vendor = resp.body as Vendor;
        this.ownerId = +this.tokenHelper.getClaimByName('accountid');
        this.map = {
          center: {lat: this.vendor.Location.Latitude, lng: this.vendor.Location.Longitude},
          zoom: 18,    
          title: this.vendor.Name,
          label: this.vendor.Name,
          markerPos: {lat: this.vendor.Location.Latitude, lng: this.vendor.Location.Longitude}    
        };   
      });
    this.vendorService.getContacts(this.vendorId)
      .then(resp => this.contacts = resp.body as Contact[])
     
  }

  createChat(){
    this.isLoaded = true;
    if(this.ownerId === undefined){
      this.ownerId = +this.tokenHelper.getClaimByName('accountid');
    }
    this.chatService.findDialog(this.ownerId, this.accountId).then(res => {
      if(res !== null){        
        this.dialog = res; 
        this.dialog.ParticipantName = this.vendor.Name + " " + this.vendor.Surname;       
        this.isLoaded = false;
        this.openChat = true;                      
      } 
      else{     
        this.dialog = {
          Id: null,
          ParticipantOneId: this.ownerId,
          ParticipantTwoId: this.accountId,
          ParticipantName: this.vendor.Name + " " + this.vendor.Surname,
          Messages: null
        };    
        this.openChat = true;
        this.isLoaded = false;
      }
      this.chatEventsService.openChat(this.dialog);     
    });
  }
}
