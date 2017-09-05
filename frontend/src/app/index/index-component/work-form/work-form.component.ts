import { Component, OnInit } from '@angular/core';

import { Category } from '../../../models/category.model';
import { Subcategory } from '../../../models/subcategory.model';

import { CategoryService } from '../../../services/category.service';

@Component({
  selector: 'app-work-form',
  templateUrl: './work-form.component.html',
  styleUrls: ['./work-form.component.sass']
})
export class WorkFormComponent implements OnInit {

  categories: Category[] = [];
  subcategories: Subcategory[] = [];

  selectedCategory: Category;
  selectedSubcategory: Subcategory;

  constructor(
    private categoryService: CategoryService
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.categoryService.getAll().then(resp => {
      this.categories = resp.body as Category[];
      
      console.log('categ', this.categories);
    }).catch(err => {
      console.log(err);
    });
  }

  onCategorySelect() {
    this.subcategories = this.selectedCategory.Subcategories;
  }

}
