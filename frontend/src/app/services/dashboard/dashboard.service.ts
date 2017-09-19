import { Injectable } from '@angular/core';

import { DataService } from '../data.service';
import { TokenHelperService } from '../helper/tokenhelper.service';

import { ShortTask } from '../../models/dashboard/company-task';
import { BookCard } from '../../models/dashboard/book-card';
import { CompanyTask } from '../../models/book/book.model';
import { Vendor } from '../../models/company-page/vendor';

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

  updateTask(book: CompanyTask): Promise<any> {
    return this.dataService.putRequest('book', book);
  }

  //vendor assign

  getCompanyVendorsWithWorks(): Promise<Vendor[]> {
    return this.dataService.getRequest(`company/${this.getId()}/dashboard/vendors`);
  }

  createTasks(tasks: ShortTask[]): Promise<any> {
    return this.dataService.postRequest(`book/company/${this.getId()}/tasks`, tasks);
  }

  getCompanyTasks(): Promise<CompanyTask[]> {
    return this.dataService.getRequest(`book/company/${this.getId()}/tasks`);
  }

  reassignVendor(task: ShortTask): Promise<any> {
    return this.dataService.putRequest(`book/company/${this.getId()}/tasks`, task);
  }

  deleteCompanyTask(id: number): Promise<any> {
    return this.dataService.deleteRequest(`book/tasks/${id}`);
  }
}
