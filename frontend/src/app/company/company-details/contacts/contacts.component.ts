import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MapModel } from "../../../models/map.model";
import { CompanyContacts } from "../../../models/company-page/company-contacts.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { Contact } from "../../../models/contact.model";
import { ChatService } from "../../../services/chat/chat.service";
import { TokenHelperService } from "../../../services/helper/tokenhelper.service";
import { DialogModel } from "../../../models/chat/dialog.model";
import { ChatEventsService } from "../../../services/events/chat-events.service";
import {DomSanitizer} from '@angular/platform-browser';

@Component({
  selector: 'company-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.sass']
})
export class ContactsComponent implements OnInit {
  @Input()
  name: string;
  @Input() isGuest: boolean;

  @Input()
  accountId: number;
  
  company: CompanyContacts;
  phones: Contact[];
  messengers: Contact[];
  socials: Contact[];
  emails: Contact[];
  
  ownerId: number;
  companyId: number;
  dialog: DialogModel;
  isLoaded: boolean = false;
  
  map: MapModel;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private chatService: ChatService,
    private tokenHelper: TokenHelperService,
    private chatEventsService: ChatEventsService,
    private sanitizer:DomSanitizer) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyContacts(params['id']))
    .subscribe(res => {       
      this.company = res;
      this.companyId = res.Id;      
      this.ownerId = +this.tokenHelper.getClaimByName('accountid');
      this.map = {
        center: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude},
        zoom: 18,    
        title: this.company.Title,
        label: this.company.Title,
        markerPos: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude}    
      };       
      this.phones = this.company.Contacts.filter(x => x.Type === "Phone");      
      this.emails = this.company.Contacts.filter(x => x.Type === "Email");
      this.messengers = this.company.Contacts.filter(x => x.Type === "Messenger");
      this.socials = this.company.Contacts.filter(x => x.Type === "Social");      
    });     
    
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
        this.dialog.ParticipantName = this.name;       
        this.isLoaded = false;              
      } 
      else{     
        this.dialog = {
          Id: null,
          ParticipantOneId: this.ownerId,
          ParticipantTwoId: this.accountId,
          ParticipantName: this.name,
          ParticipantAvatar: null,
          ParticipantType: 'company',
          ParticipantProfileId: this.companyId,
          LastMessageTime: null,
          IsReadedLastMessage: null,
          Messages: null,
          Participant1_Hided: false,
          Participant2_Hided: false
        };
        this.isLoaded = false;
      }
      this.chatEventsService.openChat(this.dialog);     
    });
  }
}
