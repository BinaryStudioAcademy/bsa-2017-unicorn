import {Vendor} from '../vendor.model'
import { Work } from "../work.model";
import { CompanyShort } from "../company-page/company-short.model";
import { Review } from '../review.model';

export interface Book {
	Date: Date;
	Description: string;
	Vendor: Vendor;
	Company: CompanyShort;
	Location: Location;
	Status: BookStatus;
	Work: Work;
}

export interface CustomerBook {
    Id: number;
    Date: Date;
    Status: BookStatus;
    Description: string;
    Performer: string;
    PerformerId: number;
    PerformerType: string;
    Rating: number;
    Review: Review;
    IsHidden: boolean;
    Work: Work;
    Location: Location;
}

export enum BookStatus {
    Pending,
    Accepted,
    Declined,
    InProgress,
    Finished,
    Confirmed
}