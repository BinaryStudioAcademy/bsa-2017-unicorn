import { Review } from './review.model';
import { Work } from './work.model';
import { PortfolioItem } from "./portfolio-item.model";

export interface Vendor {
	id: number;
	fio: string;
	avatarUrl: string;
	location: {
		adress: string;
		city: string;
		lat: number;
		lng: number;        
	};
	position: string;
	workLetter: string;
	portfolioItems: PortfolioItem,
	workList: Work[];
	rating: number;
	reviews: Review[];
}