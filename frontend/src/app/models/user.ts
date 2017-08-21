import {Book} from './book.model'
import {History} from './history'
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
	History: History[];
	Email:string;
	Phone:string;
	DateCreated: Date;
	Birthday: Date;
}