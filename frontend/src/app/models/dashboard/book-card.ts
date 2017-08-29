import { BookLocation } from './location';
import { Work } from './work';
import { Review } from '../review.model';

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
    Review: Review;
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