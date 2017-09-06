import { Review } from './review.model';
import { Work } from './work.model';
import { PortfolioItem } from "./portfolio-item.model";
import { Contact } from "./contact.model";
import { LocationModel } from './location.model'

export interface Vendor {
	Id: number;
	Name: string;
	Surname: string;
	MiddleName: string;
	Avatar: string;
	CroppedAvatar: string;
	Background: string;
	City: string;
	Location: LocationModel;
	Position: string;
	WorkLetter: string;
	ExWork: string;
	Company: string;
	CompanyId: number;
	Birthday: Date;
	AccountId: number;
}