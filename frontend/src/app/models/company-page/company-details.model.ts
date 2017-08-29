import { LocationModel } from "../location.model"
import { CompanyWork } from "./company-work.model";

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
    Works: CompanyWork[];    
    Location: LocationModel;    
}