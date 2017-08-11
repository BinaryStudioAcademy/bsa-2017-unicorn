import {Vendor} from './vendor'

export interface Book {
	date: Date;
	address: string;
	contact: string;
	description: string;
	vendor: Vendor;
	status: string;
	workType: string;


}