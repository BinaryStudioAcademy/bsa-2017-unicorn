import {Vendor} from '../vendor.model'
import { Work } from "../work.model";
import { CompanyShort } from "../company-page/company-short.model";
import { Review } from '../review.model';
import { LocationModel } from '../location.model';

export interface Book {
    Date: Date;
    EndDate: Date;
	Description: string;
    Vendor: Vendor;
	Company: CompanyShort;
	Location: LocationModel;
	Status: BookStatus;
	Work: Work;
}

export interface CompanyTask {
    Id: number;
    Date: Date;
    EndDate: Date;
    Description: string;
    DeclinedReason: string;
    IsCompanyTask: boolean;
    ParentBookId: number;
    Vendor: Vendor;
	Company: CompanyShort;
	Location: LocationModel;
	Status: BookStatus;
	Work: Work;
}

export interface CustomerBook {
    Id: number;
    Date: Date;
    EndDate: Date;
    Status: BookStatus;
    Description: string;
    Performer: string;
    PerformerId: number;
    PerformerType: string;
    DeclinedReason: string;
    Rating: number;
    Review: Review;
    IsHidden: boolean;
    Work: Work;
    Location: LocationModel;
}

export enum BookStatus {
    Pending,
    Accepted,
    Declined,
    InProgress,
    Finished,
    Confirmed
}