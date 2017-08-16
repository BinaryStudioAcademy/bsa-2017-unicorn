import { Review } from './review.model';
import { Work } from './work.model';
import { PortfolioItem } from "./portfolio-item.model";

export interface Vendor {
	Id: number;
	FIO: string;
	AvatarUrl: string;
	Location: {
		Adress: string;
		City: string;
		Lat: number;
		ng: number;        
	};
	position: string;
	workLetter: string;
	portfolioItems: PortfolioItem,
	workList: Work[];
	rating: number;
	reviews: Review[];
}