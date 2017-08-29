import { LocationModel } from '../../models/location.model'

export interface Customer {
    birthday: any;
    phone: string;
    email: string;
    image: string;
    provider: string;
    uid: string;
    firstName: string;
    middleName: string;
    lastName: string;
    location: LocationModel;
}