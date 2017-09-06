import {Book} from './book.model'
import {History} from './history'
import {LocationModel} from './location.model'
export interface User
{
	Id: number;
	Name: string;
	MiddleName: string;
	SurName: string;
	Avatar: string;
	CroppedAvatar: string;
	Background: string;
	Location : LocationModel;
	History: History[];
	Email:string;
	Phone:string;
	DateCreated: Date;
	Birthday: Date;
	AccountId: number;
}