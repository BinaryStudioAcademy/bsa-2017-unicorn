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
import {DomSanitizer} from '@angular/platform-browser';
@Component({
  selector: 'app-vendor-profile-contacts',
  templateUrl: './vendor-profile-contacts.component.html',
  styleUrls: ['./vendor-profile-contacts.component.sass'],
})
export class VendorProfileContactsComponent implements OnInit {
  @Input() private vendorId: number;
  @Input() private accountId: number;
  @Input() private isGuest: boolean;
  
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
    private chatEventsService: ChatEventsService,
    private sanitizer:DomSanitizer
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
  parseUrl(link:string):string
  {
    var newstr = link
    .replace(/.*facebook.com\//,"")
    .replace(/.*vk.com\//,"")
    .replace(/.*linkedin.com\//,"");
    return newstr;
  }
  getBaseUrl(provider:string):string
  {
    switch(provider)
    {
    case "telegram":
    return "https://telegram.me/";
    case "skype":
    return "skype:";
    case "vk":
    return "https://vk.com/";
    case "linkedin":
    return "http://www.linkedin.com/in/";
    case "facebook":
    return "https://www.facebook.com/";
    case "phone":
    return "tel:";
    case "email":
    return "mailto:";
    }
    return "";
  }
  sanitize(url:string){
    return this.sanitizer.bypassSecurityTrustUrl(url);
}
  createChat(){    
    this.isLoaded = true;
    if(this.ownerId === undefined){
      this.ownerId = +this.tokenHelper.getClaimByName('accountid');
    }
    this.chatService.findDialog(this.ownerId, this.accountId).then(res => {
      if(res !== null){        
        this.dialog = res; 
        this.dialog.ParticipantName = this.vendor.Name + " ";
        if (this.vendor.Surname) {
          this.dialog.ParticipantName += this.vendor.Surname;
        }       
        this.isLoaded = false;
        this.openChat = true;                      
      } 
      else{     
        this.dialog = {
          Id: null,
          ParticipantOneId: this.ownerId,
          ParticipantTwoId: this.accountId,
          ParticipantName: this.vendor.Name + " " + (this.vendor.MiddleName ? this.vendor.MiddleName: ''),
          ParticipantAvatar: null,
          ParticipantProfileId: this.vendorId,
          ParticipantType: "vendor",
          Messages: null,
          LastMessageTime: null,
          IsReadedLastMessage: null,
          Participant1_Hided: false,
          Participant2_Hided: false
        };    
        this.openChat = true;
        this.isLoaded = false;
      }
      this.chatEventsService.openChat(this.dialog);     
    });
  }
}
