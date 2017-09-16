import { Injectable } from '@angular/core';

import { DataService } from '../data.service';
import { TokenHelperService } from '../helper/tokenhelper.service';

import { BookCard } from '../../models/dashboard/book-card';

@Injectable()
export class DashboardService {

  constructor(
    private dataService: DataService,
    private tokenhelper: TokenHelperService) {
  }

  getRole(): string {
    return this.tokenhelper.getRoleName();
  }

  getId(): string {
    return this.tokenhelper.getClaimByName('profileid');
  }

  getPendingBooks(): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${this.getRole()}/${this.getId()}/pending`);
  }

  getAcceptedBooks(): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${this.getRole()}/${this.getId()}/accepted`);
  }

  getFinishedBooks(): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${this.getRole()}/${this.getId()}/finished`);
  }

  getPortfolioBooks(role: string, id: number): Promise<BookCard[]> {
    return this.dataService.getRequest<BookCard[]>(`book/${role}/${id}/finished`);
  }

  update(book: BookCard): Promise<any> {
    return this.dataService.putRequest('book', book);
  }

}
