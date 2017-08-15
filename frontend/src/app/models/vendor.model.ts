import { Review } from './review.model';
import { Work } from './work.model';

export interface Vendor {
	id: number;
	fio: string;
	avatarUrl: string;
	location: string;
	position: string;
	workLetter: string;

	workList: Work[];
	rating: number;
	reviewsCount: number;
	features: string[]
}