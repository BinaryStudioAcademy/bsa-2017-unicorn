import { LocationModel } from '../models/location.model';

export interface Performer {
    Id: number;
    Rating: number;
    ReviewsCount: number;
    Name: string;
    Avatar: string;
    PerformerType: string;
    Link: string;
    Location: LocationModel;
    Description: string;
}
