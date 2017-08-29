import {Vendor} from '../vendor.model'
import { Work } from "../work.model";
import { CompanyShort } from "../company-page/company-short.model";
import { LocationModel } from '../location.model';

export interface Book {
	Date: Date;
	Description: string;
	Vendor: Vendor;
	Company: CompanyShort;
	Location: LocationModel;
	Status: BookStatus;
	Work: Work;
}

export enum BookStatus {
	Accepted,
    InProgress,
    Finished,
    Confirmed
}