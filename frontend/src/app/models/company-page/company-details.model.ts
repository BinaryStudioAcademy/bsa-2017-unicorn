import { Category } from "../category.model";

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
    Categories:Category[];    
}