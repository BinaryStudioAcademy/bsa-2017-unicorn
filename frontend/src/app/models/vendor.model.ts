import { Review } from './review.model';
import { Work } from './work.model';
import { PortfolioItem } from "./portfolio-item.model";

export interface Vendor {
	Id: number;
	FIO: string;
	Avatar: string;
	City: string;
	LocationId: number;
	Position: string;
	WorkLetter: string;
	ExWork: string;
	Company: string;
	CompanyId: number;
	Works: Work[];
}