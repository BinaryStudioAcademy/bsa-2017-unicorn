import { CompanyCategory } from "./company-category.model";
import { CompanyWork } from "./company-work.model";

export interface CompanyWorks {
    Id: number;
    Works: CompanyWork[];
    AllCategories: CompanyCategory[];
}