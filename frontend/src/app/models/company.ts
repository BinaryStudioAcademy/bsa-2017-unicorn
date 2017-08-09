import { Review } from "./review";
import { Vendor } from "./vendor";

export class Company {
    avatar:string;
    name: string;
    description: string;
    foundationDate: Date;    
    location: string;
    reviews: Review[];
    rating: number;
    vendors: Vendor[];
    director: string;
    features: string[];
}