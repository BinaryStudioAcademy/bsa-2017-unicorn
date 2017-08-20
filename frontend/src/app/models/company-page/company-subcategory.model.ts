import { CompanyWork } from "./company-work.model";

export interface CompanySubcategory {
    Id: number;
    Name: string;
    Description: string;
    Works: CompanyWork[];
}