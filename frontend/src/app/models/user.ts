import {Book} from './book.model'
export class User
{
	id: number;
	isDeleted:boolean;
	firstName: string;
	middleName: string;
	surName: string;
	avatarUrl: string;
	location: string;
	books: Book[];
	gender:string;
	email:string;
	phone:string;
	dateCreated: Date;
}