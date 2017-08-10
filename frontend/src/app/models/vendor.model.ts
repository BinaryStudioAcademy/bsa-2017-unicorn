import { Review } from './review.model';
import { Work } from './work.model';

export class Vendor {
	id: number;
	fio: string;
	avatarUrl: string;
	location: string;
	rang: string;
	workLetter: string;

	workList: Work[];
	rating: number;
	reviewsCount: number;
	features: string[]
}