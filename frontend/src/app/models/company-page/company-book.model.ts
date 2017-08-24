import { CompanyWork } from "./company-work.model";
import { Location } from "../location.model"

export interface CompanyBook {
    Id: number;
    Date: Date;
    Status: BookStatus;
    Description: string;
    Customer: string;
    CustomerId: number;
    Work: CompanyWork;
    Location: Location;
}

export enum BookStatus {
	Accepted,
    InProgress,
    Finished,
    Confirmed
}