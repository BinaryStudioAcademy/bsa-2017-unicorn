import { BookLocation } from './location';
import { Work } from './work';

export interface BookCard {
    Id: number;
    Date: Date;
    Status: number;
    Description: string;
    Customer: string;
    CustomerId: number;
    CustomerPhone: string;
    Work: Work;
    Location: BookLocation;
}