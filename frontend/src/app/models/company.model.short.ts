import { Vendor } from "./vendor";
import { Contact } from "./contact.model";

export interface CompanyShort {
    Id: number,
    Avatar:string;
    Name: string;
    Description: string;    
    FoundationDate: Date;   
    Director: string; 
    Location: {
        Adress: string;
        City: string;
        Latitude: number;
        Longitude: number;        
    };          
    Vendors: Vendor[];    
    Categories:{
        Icon: string,
        Name: string
    }[];
    Contacts: Contact[];
}