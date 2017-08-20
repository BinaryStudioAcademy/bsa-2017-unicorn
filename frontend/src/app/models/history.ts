import {User} from './user'
import {Review} from './review.model'
import { Vendor } from "./company-page/vendor";

export interface History {
    date: Date;
	description: string;
    vendor: Vendor;
    user: User;
    workType: string;
    review: Review;
}