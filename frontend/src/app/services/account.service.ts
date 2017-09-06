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
		return this.dataService.getFullRequest<Notification[]>(`${this.apiController}/${id}/notifications`)
			.catch(err => alert(err));
	}

	updateNotification(accountId: number, notification: Notification): Promise<any> {
		return this.dataService.putFullRequest<Notification[]>(`${this.apiController}/${accountId}/notifications/${notification.Id}`, notification)
			.catch(err => alert(err));
	}

	removeNotification(accountId: number, notification: Notification): Promise<any> {
		return this.dataService.deleteFullRequest<Notification[]>(`${this.apiController}/${accountId}/notifications/${notification.Id}`, notification)
			.catch(err => alert(err));
	}

	searchByTemplate(template: string, count: number): Promise<ProfileShortInfo[]> {
		return this.dataService.getRequest<ProfileShortInfo[]>(`${this.apiController}/search?template=${template}&count=${count}`);
	}
}
