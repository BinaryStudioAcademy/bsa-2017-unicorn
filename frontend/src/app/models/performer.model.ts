import { Location } from '../models/location.model';

export interface Performer {
    Id: number;
    Rating: number;
    ReviewsCount: number;
    Name: string;
    Avatar: string;
    PerformerType: string;
    Link: string;
    Location: Location;
    Description: string;
}
