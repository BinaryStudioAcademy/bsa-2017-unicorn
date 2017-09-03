import { Component, OnInit } from '@angular/core';
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { CompanyContacts } from "../../../models/company-page/company-contacts.model";
import { ContactProvider } from "../../../models/contact-provider.model";
import { Contact } from "../../../models/contact.model";
import { ContactService } from "../../../services/contact.service";
import { ToastsManager, Toast } from 'ng2-toastr';
import { ToastOptions } from 'ng2-toastr';

@Component({
  selector: 'app-company-contacts',
  templateUrl: './company-contacts.component.html',
  styleUrls: ['./company-contacts.component.sass']
})
export class CompanyContactsComponent implements OnInit {  
  company: CompanyContacts;
  companyId: number;
  contacts: Contact[];
  providers: ContactProvider[];
  blockAddButtons: boolean = false;
  chatOpen: boolean = false;
  
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

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private contactService: ContactService,
    private toastr: ToastsManager) { }

  ngOnInit() {
    this.pendingContactas = [];
    this.hideAllEditFields();   
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyContacts(params['id']))
    .subscribe(res => {
      this.company = res;
      this.companyId = this.company.Id;
      this.contacts = this.company.Contacts;
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
    this.pendingContactas.push(contact);
    
    this.companyService.deleteCompanyContact(this.companyId, contact.Id)
      .then(() => {
         this.contacts.splice(this.contacts.findIndex(c => c.Id === contact.Id), 1);
         this.pendingContactas.splice(this.contacts.findIndex(c => c.Id === contact.Id), 1);
         this.filterContacts();
         this.filterProviders();
         this.toastr.success('Contact was removed', 'Success!');
      })
      .catch(() => {
         this.pendingContactas.splice(this.contacts.findIndex(c => c.Id === contact.Id), 1);
         this.toastr.error('Sorry, something went wrong', 'Error!');
      });
  }

  editPhoneOpenClickOutside()  {
    if(this.editPhoneOpen){
      this.editPhoneOpen = false;
    }   
  }

  editEmailOpenClickOutside(){
    if(this.editEmailOpen){
      this.editEmailOpen = false;
    }
  }

  editMessengerOpenClickOutside(){
    if(this.editMessengerOpen){
      this.editMessengerOpen = false;
    }
  }

  editSocialOpenClickOutside(){
    if(this.editSocialOpen){
      this.editSocialOpen = false;
    }
  }
    
  createContact(contact: Contact): void {    
    this.blockAddButtons = true;
    if (contact.ProviderId === null)
      {
        contact.ProviderId = this.selectedProvider.Id;
        contact.Provider = this.selectedProvider.Name;
        contact.Type = this.selectedProvider.Type;
      }
    this.selectedProvider = null;
    this.cleanAllSellections();
    this.hideAllEditFields();
    
    contact.Id = null;

    this.pendingContactas.push(contact);
    this.contacts.push(contact);
    this.filterContacts();  
    this.companyService.addCompanyContact(this.companyId, contact)
      .then(resp => {
        this.companyService.getCompanyContacts(this.companyId)        
        .then(res => {
          this.company = res;  
        })
          .then(() => {            
            this.contacts = this.company.Contacts;            
            this.filterContacts();
            this.pendingContactas.splice(this.pendingContactas.findIndex(c => c === contact), 1);
            this.blockAddButtons = false;
            this.toastr.success('Contact was created', 'Success!');
          }).catch(err => this.toastr.error('Sorry, something went wrong', 'Error!'));            
      });
      
  }

  updateContact(contact: Contact): void {
    this.companyService.saveCompanyContact(contact).then(res => {
      this.toastr.success('Contact was updated');
    }).catch(err => this.toastr.error('Sorry, something went wrong', 'Error!'));
    this.cleanAllSellections();
    this.hideAllEditFields();
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
    setTimeout(() => {
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
    },50);
  } 

}
