import { BookLocation } from './location';
import { Work } from '../work.model';

export interface CompanyTask {
    Id: number;
    Date: Date;
    EndDate: Date;
    Status: TaskStatus;
    BookId: number;
    Description: string;
    Customer: string;
    CustomerId: number;
    IsHidden: boolean;
    CustomerPhone: string;
    DeclinedReason: string;
    Rating: number;
    Work: Work;
    Location: BookLocation;
    VendorId: number;

}

export interface ShortTask {
    Id: number;
    BookId: number;
    Description: string;
    WorkId: number;
    DeclinedReason: string;
    VendorId: number;
}

export enum TaskStatus {
    Pending,
    Accepted,
    Declined
}