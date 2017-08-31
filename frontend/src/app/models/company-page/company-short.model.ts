import { Location } from "../location.model"
export interface CompanyShort {
    Id: number;
    AccountId: number;
    Avatar:string;
    Name: string;    
    Location: Location;
}