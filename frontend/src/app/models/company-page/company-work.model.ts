import { CompanySubcategory } from "./company-subcategory.model";

export interface CompanyWork {
    Id: number;
    Name: string;
    Description: string;
    IsIncludeToCompany: boolean;
    Subcategory: CompanySubcategory;
}