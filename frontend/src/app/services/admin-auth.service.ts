import { Injectable } from '@angular/core';

import { DataService } from '../services/data.service';

@Injectable()
export class AdminAuthService {

  constructor(private dataService: DataService) {
    this.dataService.setHeader('Content-Type', 'application/json');
  }

  public signIn(login: string, pass: string): Promise<any> {
    return this.dataService.getFullRequest(`admin/auth?login=${login}&password=${pass}`);
  }

  public switchAccount(accountId: number): Promise<any> {
    return this.dataService.getFullRequest(`switch/${accountId}`);
  }
}
