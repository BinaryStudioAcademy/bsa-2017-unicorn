import { Location } from '../../models/location.model'
export interface Company {
    foundation: any;
    phone: string;
    email: string;
    image: string;
    provider: string;
    uid: string;
    name: string;
    staff: number;
    description: string;
    location: Location;
}