import { BannedAccount } from "./banned-account.model";

export class BannedAccountsPage {
	Items: BannedAccount[];
	CurrentPage: number;
	TotalCount: number;
	PageSize: number;
} 
