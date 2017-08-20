import { Location } from "../location.model"
import { CompanyCategory } from "./company-category.model";

export interface CompanyDetails {
    Id: number;
    Avatar: string;
    Name:string;
    Description: string;
    Rating: number;
    FoundationDate: Date;   
    Director: string;   
    City: string;  
    ReviewsCount: number;
    Categories:CompanyCategory[];
    Location: Location;    
}