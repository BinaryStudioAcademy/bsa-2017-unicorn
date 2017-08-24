import { CompanySubcategory } from "./company-subcategory.model";

export interface CompanyWork {
    Id: number;
    Name: string;
    Description: string;    
    Subcategory: CompanySubcategory;
    Icon: string;
}