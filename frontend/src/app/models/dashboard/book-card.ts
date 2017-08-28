import { BookLocation } from './location';
import { Work } from './work';

export interface BookCard {
    Id: number;
    Date: Date;
    Status: BookStatus;
    Description: string;
    Customer: string;
    CustomerId: number;
    IsHidden: boolean;
    CustomerPhone: string;
    Rating: number;
    Work: Work;
    Location: BookLocation;
}

export enum BookStatus {
    Pending,
    Accepted,
    Declined,
    InProgress,
    Finished,
    Confirmed
}