import { CompanyWork } from "./company-work.model";
import { LocationModel } from "../location.model"

export interface CompanyBook {
    Id: number;
    Date: Date;
    Status: BookStatus;
    Description: string;
    Customer: string;
    CustomerId: number;
    Work: CompanyWork;
    Location: LocationModel;
}

export enum BookStatus {
	Accepted,
    InProgress,
    Finished,
    Confirmed
}