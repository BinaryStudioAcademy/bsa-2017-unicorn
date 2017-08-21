import { Work } from "../work.model";
import { BookStatus } from "./book.model";

export class VendorBook {
	Id: number;
    Date: Date;
	Status: BookStatus;
    Description: string;
	Customer: string;
	CustomerId: number;
    Work: Work;
    Location: Location;
}