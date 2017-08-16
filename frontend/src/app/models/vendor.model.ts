import { Review } from './review.model';
import { Work } from './work.model';
import { PortfolioItem } from "./portfolio-item.model";

export interface Vendor {
	Id: number;
	FIO: string;
	AvatarUrl: string;
	City: string;
	LocationId: number;
	Position: string;
	WorkLetter: string;
	ExWork: string;
	Company: string;
	CompanyId: number;
}