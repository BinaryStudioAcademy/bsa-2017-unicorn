import { Injectable } from '@angular/core';

import { DataService } from '../data.service';
import { TokenHelperService } from '../helper/tokenhelper.service';

import { BookCard } from '../../models/dashboard/book-card';

@Injectable()
export class DashboardService {

  role: string;
  id: string;

  constructor(
    private dataService: DataService,
    private tokenhelper: TokenHelperService) {
    this.initValues();
  }

  initValues() {
    this.role = this.tokenhelper.getRoleName();
    this.id = this.tokenhelper.getClaimByName('profileid');
    console.log('role ', this.role);
    console.log('id ', this.id);
  }

  getPendingBooks(): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${this.role}/${this.id}/pending`);
  }

  getAcceptedBooks(): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${this.role}/${this.id}/accepted`);
  }

  getFinishedBooks(): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${this.role}/${this.id}/finished`);
  }

}
