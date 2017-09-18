import { Injectable } from '@angular/core';

import { DataService } from "./data.service";

import { ProfileShortInfo } from "../models/profile-short-info.model";
import { Notification } from "../models/notification.model";
import { BanListPage } from "../models/admin/ban-list-page";
import { AdminAccountViewModel } from "../models/admin/admin-account-view.model";

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
			.catch(err => console.log(err));
	}
	  
	getNotifications(id: number): Promise<any> {
		return this.dataService.getFullRequest<Notification[]>(`${this.apiController}/${id}/notifications`)
			.catch(err => console.log(err));
	}

	updateNotification(accountId: number, notification: Notification): Promise<any> {
		return this.dataService.putFullRequest<Notification[]>(`${this.apiController}/${accountId}/notifications/${notification.Id}`, notification)
			.catch(err => console.log(err));
	}

	removeNotification(accountId: number, notification: Notification): Promise<any> {
		return this.dataService.deleteFullRequest<Notification[]>(`${this.apiController}/${accountId}/notifications/${notification.Id}`, notification)
			.catch(err => console.log(err));
	}

	searchByTemplate(template: string, count: number): Promise<ProfileShortInfo[]> {
		return this.dataService.getRequest<ProfileShortInfo[]>(`${this.apiController}/search?template=${template}&count=${count}`);
	}

	banAccount(id: number): Promise<any> {
		return this.dataService.postFullRequest<AdminAccountViewModel>(`${this.apiController}/${id}/ban`, null)
			.catch(err => console.log(err));
	}

	unbanAccount(id: number): Promise<any> {
		return this.dataService.postFullRequest<AdminAccountViewModel>(`${this.apiController}/${id}/unban`, null)
			.catch(err => console.log(err));
	}

	getBanListPage(page: number, size: number): Promise<BanListPage> {
		return this.dataService.getRequest<BanListPage>(`${this.apiController}/banned?page=${page}&size=${size}`);
	}

	searchInBanListByTemplate(template: string, isBanned: boolean, page: number, size: number): Promise<BanListPage> {
		let uriParams: string[] = [];

		if (template && template !== "")
			uriParams.push(`template=${template}`);

		if (isBanned !== undefined) 
			uriParams.push(`isBanned=${isBanned}`);

		
		if (page && page > 0)
			uriParams.push(`page=${page}`);
	  
		if (size && size > 0)
			uriParams.push(`size=${size}`);

		return this.dataService.getRequest<BanListPage>(`${this.apiController}/banned/search?${uriParams.join('&')}`);
	}
}
