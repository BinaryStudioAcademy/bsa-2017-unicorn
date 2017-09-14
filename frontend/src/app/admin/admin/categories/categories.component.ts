import { Component, OnInit } from '@angular/core';
import { CategoryService } from "../../../services/category.service";
import { Category } from "../../../models/category.model";
import { Subcategory } from "../../../models/subcategory.model";

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.sass']
})
export class CategoriesComponent implements OnInit {

  constructor(private categoryService: CategoryService) { }

  categories: Category[];

  editingCategory: Category;
  editingSubcategory: Subcategory;

  isLoaded: boolean;
  pendingCategories: Category[];
  pendingSubcategories: Subcategory[];

  ngOnInit() {
    this.load();
  }

  load(): void {
    this.isLoaded = false;

    this.clearEditingCategory();
    this.clearEditingSubcategory();

    this.pendingCategories = [];
    this.pendingSubcategories = [];

    this.categoryService.getAll()
      .then(resp => {
        this.categories = resp.body as Category[];
        this.isLoaded = true;
      })
      .catch(err => this.isLoaded = true);
  }

  saveCategory(): void {
    if (this.editingCategory.Id !== null) {
      this.categoryService.updateCategory(this.editingCategory);
    }
    else
    {
      let category = this.editingCategory;
      category.Id = -1;
      this.clearEditingCategory()

      this.pendingCategories.push(category);
      this.categories.push(category);

      this.categoryService.createCategory(category)
        .then(resp => {          
          this.pendingCategories.splice(this.pendingCategories.findIndex(c => c === category), 1);
          category.Id = resp.body.Id;
        })
        .catch(err => {
          this.categories.splice(this.categories.findIndex(c => c === category), 1);
          this.pendingCategories.splice(this.pendingCategories.findIndex(c => c === category), 1);
        });
    }
  }

  saveSubcategory(): void {
    if (this.editingSubcategory.Id !== null) {
      this.categoryService.updateSubcategory(this.editingSubcategory.CategoryId, this.editingSubcategory);
    }
    else
    {
      let subcategory = this.editingSubcategory;
      subcategory.Id = -1;
      this.clearEditingSubcategory()

      this.pendingSubcategories.push(subcategory);
      this.categories
        .find(c => c.Id === subcategory.CategoryId).Subcategories
        .push(subcategory);

      this.categoryService.createSubcategory(subcategory.CategoryId, subcategory)
        .then(resp => {          
          this.pendingSubcategories.splice(this.pendingSubcategories.findIndex(s => s === subcategory), 1);
          subcategory.Id = resp.body.Id;
        })
        .catch(err => {
          let category = this.categories
            .find(c => c.Id === subcategory.CategoryId);
          category.Subcategories
            .splice(category.Subcategories.findIndex(s => s === subcategory), 1);
          this.pendingSubcategories.splice(this.pendingSubcategories.findIndex(s => s === subcategory), 1);
        });
    }
  }

  removeCategory(category: Category): void {
    this.pendingCategories.push(category);

    this.categoryService.removeCategory(category.Id)
      .then(() => {
        this.categories.splice(this.categories.findIndex(c => c === category), 1);
        this.pendingCategories.splice(this.pendingCategories.findIndex(c => c === category), 1);
      })
      .catch(err => this.pendingCategories.splice(this.pendingCategories.findIndex(c => c === category), 1));
  }

  removeSubcategory(subcategory: Subcategory): void {
    this.pendingSubcategories.push(subcategory);

    let category = this.categories
      .find(c => c.Id === subcategory.CategoryId);
    
    this.categoryService.removeSubcategory(subcategory.CategoryId, subcategory.Id)
      .then(() => {
        category.Subcategories
          .splice(category.Subcategories.findIndex(s => s === subcategory), 1);
        this.pendingSubcategories.splice(this.pendingSubcategories.findIndex(s => s === subcategory), 1);
      })
      .catch(err => this.pendingSubcategories.splice(this.pendingSubcategories.findIndex(s => s === subcategory), 1));
  }

  clearEditingCategory(): void {
    this.editingCategory.Id = null;
    this.editingCategory.Description = "";
    this.editingCategory.Icon = "http://www.freeiconspng.com/uploads/pictures-icon-11.gif",
    this.editingCategory.Name = "";
    this.editingCategory.Tags = "";
    this.editingCategory.Subcategories = []
  }

  clearEditingSubcategory(): void {
    this.editingSubcategory.Description = "";
    this.editingSubcategory.Icon = "http://www.freeiconspng.com/uploads/pictures-icon-11.gif",
    this.editingSubcategory.Name = "";
    this.editingSubcategory.Tags = "";
    this.editingSubcategory.Category = "";
    this.editingSubcategory.CategoryId = null;
    this.editingSubcategory.Id = null;
  }

  isCategoryOnPending(category: Category): boolean {
    return this.pendingCategories.includes(category);
  }

  isSubcategoryOnPending(subcategory: Subcategory): boolean {
    return this.pendingSubcategories.includes(subcategory);
  }

}
