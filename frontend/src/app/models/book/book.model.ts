import {Vendor} from '../vendor.model'
import { Work } from "../work.model";
import { CompanyShort } from "../company-page/company-short.model";

export interface Book {
	Date: Date;
	Description: string;
	Vendor: Vendor;
	Company: CompanyShort;
	Location: Location;
	Status: BookStatus;
	Work: Work;
}

export enum BookStatus {
	Accepted,
    InProgress,
    Finished,
    Confirmed
}