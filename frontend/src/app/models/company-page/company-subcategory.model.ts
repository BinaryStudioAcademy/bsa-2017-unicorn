import { CompanyCategory } from "./company-category.model";
import { CompanyWork } from "./company-work.model";

export interface CompanySubcategory {
    Id: number;
    Name: string;
    Description: string;
    Category: CompanyCategory;
    Works: CompanyWork[];
}