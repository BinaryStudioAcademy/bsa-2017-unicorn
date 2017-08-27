import { Location } from '../../models/location.model'

export interface Vendor {
    birthday: any;
    phone: string;
    email: string;
    image: string;
    provider: string;
    uid: string;
    firstName: string;
    middleName: string;
    lastName: string;
    experience: number;
    position: string;
    speciality: string;
    location: Location;
}