import { LocationModel } from '../location.model';

export interface BookOrder {
    date: Date;
    description: string;
    customerid: number;
    location: LocationModel;
    customerphone: string;
    profile: string;
    profileid: number;
    workid: number;
}
