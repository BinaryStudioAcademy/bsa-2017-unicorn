import { CompanySubcategory } from "./company-subcategory.model";

export interface CompanyCategory {
    Id: number;
    Icon: string;
    Name: string;
    Description: string;
    Subcategories: CompanySubcategory[];
}