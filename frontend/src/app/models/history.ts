import {Vendor} from './vendor'
import {User} from './user'
import {Review} from './review.model'

export class History {
    date: Date;
	description: string;
    vendor: Vendor;
    user: User;
    workType: string;
    review: Review;
}