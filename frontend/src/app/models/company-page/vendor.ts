import { Work } from '../work.model';

export interface Vendor{
    Id: number;
    Avatar: string;
    Background: string;
    Experience: number;
    Position: string;
    FIO: string;
    Reviews: number;
    Rating: number;
    Works: Work[];
}