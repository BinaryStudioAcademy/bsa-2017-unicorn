import { Work } from "../work.model";
import { BookStatus } from "./book.model";
import { LocationModel } from '../location.model';

export class VendorBook {
	Id: number;
    Date: Date;
    EndDate: Date;
	Status: BookStatus;
    Description: string;
    Customer: string;
    CustomerPhone: string;
	CustomerId: number;
    Work: Work;
    Location: LocationModel;
    MoreTasksPerDay: boolean;
}