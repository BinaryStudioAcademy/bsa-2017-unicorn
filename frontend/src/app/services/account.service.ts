import { Injectable } from '@angular/core';

import { DataService } from "./data.service";

import { ProfileShortInfo } from "../models/profile-short-info.model";
import { Notification } from "../models/notification.model";
import { BannedAccount } from "../models/admin/banned-account.model";
import { BannedAccountsPage } from "../models/admin/banned-accounts-page.model";

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

	banAccount(id: number, endTime: Date): Promise<any> {
		return this.dataService.postFullRequest<BannedAccount>(`${this.apiController}/${id}/ban`, endTime)
			.catch(err => alert(err));
	}

	updateBan(accountId: number, endTime: Date): Promise<any> {
		return this.dataService.putFullRequest<BannedAccount>(`${this.apiController}/${accountId}/ban`, endTime)
			.catch(err => alert(err));
	}

	unbanAccount(accountId: number): Promise<any> {
		return this.dataService.deleteRequest<BannedAccount>(`${this.apiController}/${accountId}/unban`)
			.catch(err => alert(err));
	}

	getBannedAccountsPage(page: number, size: number): Promise<BannedAccountsPage> {
		return this.dataService.getRequest<BannedAccountsPage>(`${this.apiController}/banned?page=${page}&size=${size}`);
	}

	searchBannedByTemplate(template: string, page: number, size: number): Promise<ProfileShortInfo[]> {
		return this.dataService.getRequest<ProfileShortInfo[]>(`${this.apiController}/banned/search?template=${template}&page=${page}&size=${size}`);
	}
}
