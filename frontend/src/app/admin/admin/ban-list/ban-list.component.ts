import { Component, OnInit } from '@angular/core';

import { AdminAccountViewModel } from "../../../models/admin/admin-account-view.model";
import { AccountService } from "../../../services/account.service";

@Component({
  selector: 'app-ban-list',
  templateUrl: './ban-list.component.html',
  styleUrls: ['./ban-list.component.sass']
})
export class BanListComponent implements OnInit {

  accounts: AdminAccountViewModel[];
  isBanned: boolean;
  page: number = 1;
  pageSize: number = 20;
  totalCount: number = 0;

  pendingAccounts: AdminAccountViewModel[];

  isLoaded: boolean;

  searchTemplate: string = "";

  constructor(private accountService: AccountService) { }

  ngOnInit() {
    this.load();
    this.pendingAccounts = [];
    this.isBanned = undefined;
  }

  load(): void {
    this.isLoaded = false;
    this.pendingAccounts = [];
    this.accountService.searchInBanListByTemplate(this.searchTemplate, this.isBanned, this.page, this.pageSize)
      .then(p => {
        this.page = p.CurrentPage;
        this.pageSize = p.PageSize;
        this.totalCount = p.TotalCount;
        this.accounts = p.Items;

        this.isLoaded = true;
      })
      .catch(err => this.isLoaded = true);
  }

  isAccountPending(account: AdminAccountViewModel): boolean {
    return this.pendingAccounts.includes(account);
  }

  banAccount(account: AdminAccountViewModel): void {
    this.pendingAccounts.push(account);

    this.accountService.banAccount(account.Id)
      .then(() => {
        account.IsBanned = true;
        this.pendingAccounts.splice(this.pendingAccounts.findIndex(a => a.Id === account.Id), 1);
      })
      .catch(err => this.pendingAccounts.splice(this.pendingAccounts.findIndex(a => a.Id === account.Id), 1));
  }

  unbanAccount(account: AdminAccountViewModel): void {
    this.pendingAccounts.push(account);

    this.accountService.unbanAccount(account.Id)
      .then(() => {
        account.IsBanned = false;
        this.pendingAccounts.splice(this.pendingAccounts.findIndex(a => a.Id === account.Id), 1);
      })
      .catch(err => this.pendingAccounts.splice(this.pendingAccounts.findIndex(a => a.Id === account.Id), 1));
  }

}
