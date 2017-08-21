import {Vendor} from '../vendor.model'
import { Work } from "../work.model";
<<<<<<< HEAD
import { Company } from "../company.model";
=======
import { CompanyShort } from "../company-page/company-short.model";
>>>>>>> develop

export interface Book {
	Date: Date;
	Description: string;
	Vendor: Vendor;
<<<<<<< HEAD
	Company: Company;
=======
	Company: CompanyShort;
>>>>>>> develop
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