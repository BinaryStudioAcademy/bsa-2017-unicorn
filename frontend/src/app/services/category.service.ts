import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Category } from "../models/category.model";
import { Subcategory } from "../models/subcategory.model";

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
		  .catch(err => console.log(err));
  }

  search(template: string): Promise<Category[]> {
	  return this.dataService.getRequest<Category[]>(`${this.apiController}/search?template=${template}`);
  }

  createCategory(category: Category): Promise<any> {
    return this.dataService.postFullRequest<Category>(`${this.apiController}`, category)
      .catch(err => console.log(err));
  }

  updateCategory(category: Category): Promise<any> {
    return this.dataService.putFullRequest(`${this.apiController}/${category.Id}`, category)
      .catch(err => console.log(err));
  }

  removeCategory(id: number): Promise<any> {
    return this.dataService.deleteRequest(`${this.apiController}/${id}`)
      .catch(err => console.log(err));
  }

  createSubcategory(categoryId: number, subcategory: Subcategory): Promise<any> {
    return this.dataService.postFullRequest<Category>(`${this.apiController}/${categoryId}/subcategories`, subcategory)
      .catch(err => console.log(err));
  }

  updateSubcategory(categoryId: number, subcategory: Subcategory): Promise<any> {
    return this.dataService.putFullRequest(`${this.apiController}/${categoryId}/subcategories/${subcategory.Id}`, subcategory)
      .catch(err => console.log(err));
  }

  removeSubcategory(categoryId: number, id: number): Promise<any> {
    return this.dataService.deleteRequest(`${this.apiController}/${categoryId}/subcategories/${id}`)
      .catch(err => console.log(err));
  }
}