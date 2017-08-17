import { Review } from "./review.model";
import { Vendor } from "./vendor";
import { Contact } from "./contact.model";

export interface Company {
    Avatar:string;
    Name: string;
    Description: string;
    Rating: number;
    FoundationDate: Date;   
    Director: string; 
    Location: {
        Adress: string;
        City: string;
        Latitude: number;
        Longitude: number;        
    };    
    Reviews: Review[];    
    Vendors: Vendor[];    
    Categories:{
        Icon: string,
        Name: string
    }[];
    Contacts: Contact[];
}