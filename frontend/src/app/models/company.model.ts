import { Review } from "./review.model";
import { Vendor } from "./vendor";

export interface Company {
    Avatar:string;
    Name: string;
    Description: string;
    FoundationDate: Date;    
    Location: {
        Adress: string;
        City: string;
        Latitude: number;
        Longitude: number;        
    };
    Rating: number;
    Reviews: Review[];    
    Vendors: Vendor[];
    Director: string;
    Categories:{
        Icon: string,
        Name: string
    }[];
}