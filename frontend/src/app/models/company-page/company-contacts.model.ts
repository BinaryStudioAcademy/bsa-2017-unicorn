import { LocationModel } from "../location.model"
import { Contact } from "../contact.model";
export interface CompanyContacts {
    Id: number;
    Title: string;
    Location: LocationModel;   
    Contacts: Contact[];
}