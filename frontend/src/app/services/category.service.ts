import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Category } from "../models/category.model";

@Injectable()
export class CategoryService {
  private apiController: string;

  constructor(private dataService: DataService) 
  { 
    dataService.setHeader('Content-Type', 'application/json');
    this.apiController = "categories";
  }

  getAll(): Promise<any> {
	  return this.dataService.getFullRequest<Category[]>(this.apiController)
		  .catch(err => alert(err));
  }
}