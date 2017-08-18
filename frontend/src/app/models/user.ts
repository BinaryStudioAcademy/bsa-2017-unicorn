import {Book} from './book.model'
export interface User
{
	id: number;
	Name: string;
	MiddleName: string;
	SurName: string;
	Avatar: string;
	LocationId : string;
	Books: Book[];
	Email:string;
	Phone:string;
	DateCreated: Date;
	Birthday: Date;
}