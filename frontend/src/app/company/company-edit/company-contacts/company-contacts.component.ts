import { Component, OnInit } from '@angular/core';
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyContacts } from "../../../models/company-page/company-contacts.model";
import { ContactProvider } from "../../../models/contact-provider.model";
import { Contact } from "../../../models/contact.model";
import { ContactService } from "../../../services/contact.service";

@Component({
  selector: 'app-company-contacts',
  templateUrl: './company-contacts.component.html',
  styleUrls: ['./company-contacts.component.sass']
})
export class CompanyContactsComponent implements OnInit {
  company: CompanyContacts;
  providers: ContactProvider[];
  
  phones: Contact[];
  messengers: Contact[];
  socials: Contact[];
  emails: Contact[];

  messengerProviders: ContactProvider[];
  socialProviders: ContactProvider[];

  selectedPhone: Contact;
  selectedMessenger: Contact;
  selectedEmail: Contact;
  selectedSocial: Contact;

  selectedProvider: ContactProvider;

  editPhoneOpen: boolean;
  editSocialOpen: boolean;
  editEmailOpen: boolean;
  editMessengerOpen: boolean;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private contactService: ContactService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyContacts(params['id']))
    .subscribe(res => {
      this.company = res;       
      this.filterContacts();
      this.contactService.getAllProviders()
        .then((resp) => {
          this.providers = resp.body as ContactProvider[];
          this.filterProviders();
          this.cleanAllSellections();
        })
    });
  }

  filterContacts(): void {
    this.phones = this.company.Contacts.filter(x => x.Type === "Phone");
    this.emails = this.company.Contacts.filter(x => x.Type === "Email");
    this.messengers = this.company.Contacts.filter(x => x.Type === "Messenger");
    this.socials = this.company.Contacts.filter(x => x.Type === "Social");
  }

  filterProviders(): void {
    this.messengerProviders = this.providers
      .filter(x => x.Type === "Messenger")
      .filter(x => this.messengers.find(m => m.ProviderId === x.Id) === undefined);
    this.socialProviders = this.providers
      .filter(x => x.Type === "Social")
      .filter(x => this.socials.find(s => s.ProviderId === x.Id) === undefined);
  }

  removeContact(contact: Contact): void {
    // this.vendorService.removeContact(this.vendorId, contact);
    this.company.Contacts.splice(this.company.Contacts.findIndex(c => c.Id === contact.Id), 1);
    this.filterContacts();
    this.filterProviders();
  }

  createContact(contact: Contact): void {
    if (contact.ProviderId === null)
      {
        contact.ProviderId = this.selectedProvider.Id;
        contact.Provider = this.selectedProvider.Name;
        contact.Type = this.selectedProvider.Type;
      }
    this.selectedProvider = null;
    this.cleanAllSellections();

    this.company.Contacts.push(contact);
    this.filterContacts();
    this.filterProviders();

    // this.vendorService.postVendorContact(this.vendorId, contact)
    //   .then(resp => this.contacts = resp.body as Contact[])
    //   .then(() => this.filterContacts())
    //   .then(() => this.filterProviders());
  }

  updateContact(contact: Contact): void {
    // this.vendorService.updateContact(this.vendorId, contact);
    this.company.Contacts.splice(this.company.Contacts.findIndex(x => x.Id === contact.Id), 1, contact);
    this.cleanAllSellections();
  }

  saveContacts(){
    // this.company = undefined;
    // this.companyService.saveCompanyContacts(this.company).then(() => {
    //   this.ngOnInit();
    // });
  }

  onPhoneSelect(phone: Contact): void {
    this.cleanAllSellections();
    this.selectedPhone = phone;
    this.hideAllEditFields();
    
  }

  onEmailSelect(email: Contact): void {
    this.cleanAllSellections();
    this.selectedEmail = email;
    this.hideAllEditFields();
  }

  onMessengerSelect(messenger: Contact): void {
    this.cleanAllSellections();
    this.selectedMessenger = messenger;
    this.hideAllEditFields();
  }

  onSocialSelect(social: Contact): void {
    this.cleanAllSellections();
    this.selectedSocial = social;
    this.hideAllEditFields();
  }

  cleanAllSellections(): void {
    this.cleanSelectedEmail();
    this.cleanSelectedMessenger();
    this.cleanSelectedPhone();
    this.cleanSelectedSocial();
  }

  hideAllEditFields(): void {
    this.editPhoneOpen = false;
    this.editSocialOpen = false;
    this.editEmailOpen = false;
    this.editMessengerOpen = false;
  }

  cleanSelectedPhone(): void {
    var provider = this.providers.find(p => p.Name === "phone");
    this.selectedPhone = {
      Provider: provider.Name,
      ProviderId: provider.Id,
      Type: provider.Type,
      Value: "",
      Id: null
    };
  }

  cleanSelectedEmail(): void {
    var provider = this.providers.find(p => p.Name === "email");
    this.selectedEmail = {
      Provider: provider.Name,
      ProviderId: provider.Id,
      Type: provider.Type,
      Value: "",
      Id: null
    };
  }

  cleanSelectedMessenger(): void {
    this.selectedMessenger = {
      Provider: "",
      ProviderId: null,
      Type: "",
      Value: "",
      Id: null
    };
  }

  cleanSelectedSocial(): void {
    this.selectedSocial = {
      Provider: "",
      ProviderId: null,
      Type: "",
      Value: "",
      Id: null
    };
  }

  editToggle(contactType: string): void {
    this.cleanAllSellections();

    switch (contactType) {
      case 'phone':
        this.hideAllEditFields();
        this.editPhoneOpen = true;
        break;
      case 'email':
        this.hideAllEditFields();
        this.editEmailOpen = true;
        break;
      case 'messenger':
        this.hideAllEditFields();
        this.editMessengerOpen = true;
        break;
      case 'social':
        this.hideAllEditFields();
        this.editSocialOpen = true;
        break;
    }
  }

}
