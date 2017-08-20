import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MapModel } from "../../../models/map.model";
import { CompanyContacts } from "../../../models/company-page/company-contacts.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { Contact } from "../../../models/contact.model";


export interface IContext {
    data:string;
}

@Component({
  selector: 'company-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.sass']
})
export class ContactsComponent implements OnInit {    
  company: CompanyContacts;
  phones: Contact[];
  messengers: Contact[];
  socials: Contact[];
  emails: Contact[];
  
  map: MapModel;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute) { }

  ngOnInit() { 
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyContacts(params['id']))
    .subscribe(res => {      
      this.company = res;
      console.log(this.company);
      this.map = {
        center: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude},
        zoom: 18,    
        title: this.company.Title,
        label: this.company.Title,
        markerPos: {lat: this.company.Location.Latitude, lng: this.company.Location.Longitude}    
      }; 

      //console.log(this.company.Contacts);
      this.phones = this.company.Contacts.filter(x => x.Type === "Phone");
      //console.log(this.phones);
      this.emails = this.company.Contacts.filter(x => x.Type === "Email");
      this.messengers = this.company.Contacts.filter(x => x.Type === "Messenger");
      this.socials = this.company.Contacts.filter(x => x.Type === "Social");

    });     
  }   
}
