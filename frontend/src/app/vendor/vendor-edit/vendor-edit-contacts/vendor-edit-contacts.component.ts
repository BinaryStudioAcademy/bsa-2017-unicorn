import { Component, OnInit, Input } from '@angular/core';
import { Contact } from "../../../models/contact.model";
import { VendorService } from "../../../services/vendor.service";
import { ContactService } from "../../../services/contact.service";
import { ContactProvider } from "../../../models/contact-provider.model";

@Component({
  selector: 'app-vendor-edit-contacts',
  templateUrl: './vendor-edit-contacts.component.html',
  styleUrls: ['./vendor-edit-contacts.component.sass']
})
export class VendorEditContactsComponent implements OnInit {
  @Input() private vendorId: number;
  
  contacts: Contact[];
  providers: ContactProvider[];
  
  phones: Contact[];
  messengers: Contact[];
  socials: Contact[];
  emails: Contact[];

  messengerProviders: ContactProvider[];
  socialProviders: ContactProvider[];

  pendingContactas: Contact[];

  selectedPhone: Contact;
  selectedMessenger: Contact;
  selectedEmail: Contact;
  selectedSocial: Contact;

  selectedProvider: ContactProvider;

  editPhoneOpen: boolean;
  editSocialOpen: boolean;
  editEmailOpen: boolean;
  editMessengerOpen: boolean;

  constructor(
    private vendorService: VendorService,
    private contactService: ContactService
  ) { }

  ngOnInit() {
    this.pendingContactas = [];
    this.hideAllEditFields();
    this.vendorService.getContacts(this.vendorId)
      .then(resp => this.contacts = resp.body as Contact[])
      .then(() => this.filterContacts())
      .then(() => this.contactService.getAllProviders()
        .then((resp) => this.providers = resp.body as ContactProvider[])
        .then(() => this.filterProviders())
        .then(() => this.cleanAllSellections()));
  }

  filterContacts(): void {
    this.phones = this.contacts.filter(x => x.Type === "Phone");
    this.emails = this.contacts.filter(x => x.Type === "Email");
    this.messengers = this.contacts.filter(x => x.Type === "Messenger");
    this.socials = this.contacts.filter(x => x.Type === "Social");
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
    this.vendorService.removeContact(this.vendorId, contact);
    this.contacts.splice(this.contacts.findIndex(c => c.Id === contact.Id), 1);
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

    contact.Id = null;

    this.pendingContactas.push(contact);
    this.contacts.push(contact);
    this.filterContacts()
    this.filterContacts()

    this.vendorService.postVendorContact(this.vendorId, contact)
      .then(resp => {
        this.pendingContactas.splice(this.pendingContactas.findIndex(c => c === contact), 1);
        contact.Id = (resp.body as Contact).Id;
      })
      .then(() => this.filterContacts())
      .then(() => this.filterContacts());
  }

  updateContact(contact: Contact): void {
    this.vendorService.updateContact(this.vendorId, contact);
    this.cleanAllSellections();
  }

  isContactPending(contact: Contact): boolean {
    return this.pendingContactas.includes(contact);
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
      Id: -1
    };
  }

  cleanSelectedEmail(): void {
    var provider = this.providers.find(p => p.Name === "email");
    this.selectedEmail = {
      Provider: provider.Name,
      ProviderId: provider.Id,
      Type: provider.Type,
      Value: "",
      Id: -1
    };
  }

  cleanSelectedMessenger(): void {
    this.selectedMessenger = {
      Provider: "",
      ProviderId: null,
      Type: "",
      Value: "",
      Id: -1
    };
  }

  cleanSelectedSocial(): void {
    this.selectedSocial = {
      Provider: "",
      ProviderId: null,
      Type: "",
      Value: "",
      Id: -1
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
