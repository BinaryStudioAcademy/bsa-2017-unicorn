import { Injectable } from '@angular/core';

import { DataService } from "./data.service";

import { ContactProvider } from "../models/contact-provider.model";

@Injectable()
export class ContactService {
	private apiController: string;

  	constructor(private dataService: DataService) 
  	{ 
    	dataService.setHeader('Content-Type', 'application/json');
    	this.apiController = "contacts";
  	}

  	getAllProviders(): Promise<any> {
		return this.dataService.getFullRequest<ContactProvider>(`${this.apiController}/providers`)
			.catch(err => console.log(err));
  	}
}
