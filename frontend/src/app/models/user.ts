import {Book} from './book.model'
export interface User
{
	Id: number;
	Name: string;
	MiddleName: string;
	SurName: string;
	Avatar: string;
	Background: string;
	LocationId : string;
	Books: Book[];
	Email:string;
	Phone:string;
	DateCreated: Date;
	Birthday: Date;
}