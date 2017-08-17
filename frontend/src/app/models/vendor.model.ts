import { Review } from './review.model';
import { Work } from './work.model';
import { PortfolioItem } from "./portfolio-item.model";
import { Contact } from "./contact.model";

export interface Vendor {
	Id: number;
	Name: string;
	Surname: string;
	MiddleName: string;
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