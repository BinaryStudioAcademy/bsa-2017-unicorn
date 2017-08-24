import { Location } from '../location.model';

export interface BookOrder {
    date: Date;
    description: string;
    customerid: number;
    location: Location;
    customerphone: string;
    profile: string;
    profileid: number;
    workid: number;
}
