import { LocationModel } from '../location.model';
export interface SearchWork {
    Id: number;
    Rating: number;
    ReviewsCount: number;
    Name: string;
    Avatar: string;
    PerformerType: string;
    PerformerName: string;
    Link: string;
    Location: LocationModel;
    Description: string;
    Distance: number;
}
