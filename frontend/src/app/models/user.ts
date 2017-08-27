import {Book} from './book.model'
import {History} from './history'
import {Location} from './location.model'
export interface User
{
	Id: number;
	Name: string;
	MiddleName: string;
	SurName: string;
	Avatar: string;
	Background: string;
	Location : Location;
	History: History[];
	Email:string;
	Phone:string;
	DateCreated: Date;
	Birthday: Date;
}