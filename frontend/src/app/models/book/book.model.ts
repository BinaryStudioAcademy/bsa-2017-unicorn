import {Vendor} from '../vendor.model'
import { Work } from "../work.model";
import { Company } from "../company.model";

export interface Book {
	Date: Date;
	Description: string;
	Vendor: Vendor;
	Company: Company;
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