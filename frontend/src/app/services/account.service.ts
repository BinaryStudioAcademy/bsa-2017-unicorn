import { Injectable } from '@angular/core';

import { DataService } from "./data.service";

import { ProfileShortInfo } from "../models/profile-short-info.model";
import { Notification } from "../models/notification.model";

@Injectable()
export class AccountService {
	private apiController: string;

  	constructor(private dataService: DataService) 
  	{ 
    	dataService.setHeader('Content-Type', 'application/json');
    	this.apiController = "account";
  	}

  	getShortInfo(id: number): Promise<any> {
		return this.dataService.getFullRequest<ProfileShortInfo>(`${this.apiController}/${id}`)
			.catch(err => alert(err));
	}
	  
	getNotifications(id: number): Promise<any> {
		return this.dataService.getFullRequest<Notification>(`${this.apiController}/${id}/notifications`)
		.catch(err => alert(err));
	}
}
