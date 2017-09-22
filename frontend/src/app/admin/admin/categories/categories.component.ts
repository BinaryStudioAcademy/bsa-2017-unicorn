import { Component, OnInit, ViewChild, NgZone, ChangeDetectorRef, HostListener } from '@angular/core';

import {SuiModalService, TemplateModalConfig, ModalTemplate} from 'ng2-semantic-ui';

import { CategoryService } from "../../../services/category.service";

import { Category } from "../../../models/category.model";
import { Subcategory } from "../../../models/subcategory.model";
import { ImageCropperModal } from '../../../image-cropper-modal/image-cropper-modal.component';
import { ConfirmModal } from '../../../confirm-modal/confirm-modal.component';

export interface ICategoryModalContext {
  category: Category;
}

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.sass']
})
export class CategoriesComponent implements OnInit {
  
  constructor(
    private categoryService: CategoryService,
    public modalService: SuiModalService,
    private zone: NgZone,
    private changeDetector: ChangeDetectorRef
  ) { }
  
  @HostListener('window:resize', ['$event'])
  onResize(event) {
    this.screenWidth = window.screen.width;
  }

  categories: Category[];

  selectedCategory: Category;
  selectedSubcategory: Subcategory;
  isNewCategoryEditOpen: boolean;
  isNewSubcategoryEditOpen: boolean;

  isLoaded: boolean;
  pendingCategories: Category[];
  pendingSubcategories: Subcategory[];

  openedCategoryPanels: boolean[];
  
  searchTemplate: string;

  screenWidth: number = window.screen.width;

  ngOnInit() {
    this.openedCategoryPanels = [];
    this.searchTemplate = "";
    this.load();
  }

  load(): void {
    this.isLoaded = false;

    this.clearSelectedCategory();
    this.clearSelectedSubcategory();

    this.pendingCategories = [];
    this.pendingSubcategories = [];

    this.categoryService.getAll()
      .then(resp => {
        this.categories = resp.body as Category[];
        this.isLoaded = true;
      })
      .catch(err => this.isLoaded = true);
  }

  search(): void {
    if (this.searchTemplate === "") {
      this.load();
    }
    else {
      this.categoryService.search(this.searchTemplate)
        .then(resp => {
          this.clearSelectedCategory();
          this.clearSelectedSubcategory();

          this.pendingCategories = [];
          this.pendingSubcategories = [];

          this.categories = resp;
        });
    }
  }

  saveCategory(): void {
    if (this.selectedCategory.Id !== null) {
      this.categoryService.updateCategory(this.selectedCategory);
    }
    else
    {
      let category = this.selectedCategory;
      category.Id = -1;
      category.Tags = `${category.Name},${category.Tags}`;
      this.clearSelectedCategory()

      this.pendingCategories.push(category);
      this.categories.unshift(category);

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
    this.isNewCategoryEditOpen = false;
    this.zone.run(() => {this.isNewCategoryEditOpen = false});

    this.clearSelectedCategory();
    this.clearSelectedSubcategory();
  }

  saveSubcategory(): void {
    if (this.selectedSubcategory.Id !== null) {
      this.categoryService.updateSubcategory(this.selectedSubcategory.CategoryId, this.selectedSubcategory);
    }
    else
    {
      let subcategory = this.selectedSubcategory;
      subcategory.Id = -1;
      subcategory.Tags = `${subcategory.Name},${subcategory.Tags}`;
      this.clearSelectedSubcategory()

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

    this.isNewSubcategoryEditOpen = false;
    this.zone.run(() => {this.isNewSubcategoryEditOpen = false});

    this.clearSelectedCategory();
    this.clearSelectedSubcategory();
  }

  removeCategory(category: Category): void {
    this.modalService.open(new ConfirmModal("Delete category", `Delete category ${category.Name}?`, "Delete"))
      .onApprove(() => {
        this.openedCategoryPanels = [];
        this.pendingCategories.push(category);
        
        this.categoryService.removeCategory(category.Id)
          .then(() => {
            this.categories.splice(this.categories.findIndex(c => c === category), 1);
            this.pendingCategories.splice(this.pendingCategories.findIndex(c => c === category), 1);
          })
          .catch(err => this.pendingCategories.splice(this.pendingCategories.findIndex(c => c === category), 1));

        this.isNewCategoryEditOpen = false;
        this.zone.run(() => { this.isNewCategoryEditOpen = false });

        this.clearSelectedCategory();
        this.clearSelectedSubcategory();
      });
  }

  removeSubcategory(subcategory: Subcategory): void {
    this.modalService.open(new ConfirmModal("Delete subcategory", `Delete subcategory ${subcategory.Name} from ${subcategory.Category}?`, "Delete"))
      .onApprove(() => {
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

        this.isNewSubcategoryEditOpen = false;
        this.zone.run(() => { this.isNewSubcategoryEditOpen = false });

        this.clearSelectedCategory();
        this.clearSelectedSubcategory();
      });
  }

  editCategory(category: Category): void {
    this.clearSelectedCategory();
    this.clearSelectedSubcategory();

    this.openedCategoryPanels = [];

    if (category) {
      this.isNewCategoryEditOpen = false; 
      this.selectedCategory = category;
    }
    else {
      this.selectedCategory.Id = null;
      this.isNewCategoryEditOpen = !this.isNewCategoryEditOpen;
    } 
  }

  editSubcategory(category: Category, subcategory: Subcategory): void {
    this.clearSelectedSubcategory();
    this.clearSelectedCategory();

    if (subcategory) {
      this.selectedSubcategory = subcategory;
      this.isNewSubcategoryEditOpen = false;
      this.zone.run(() => {this.isNewSubcategoryEditOpen = false});
    }
    else {
      this.selectedSubcategory.Category = category.Name;
      this.selectedSubcategory.CategoryId = category.Id;
      setTimeout(() => {
        this.isNewSubcategoryEditOpen = true;
        this.zone.run(() => { this.isNewSubcategoryEditOpen = true })
      });
    }
  }

  selectImage(): void {
    this.modalService.open(new ImageCropperModal())
      .onApprove(result => this.selectedCategory.Icon = result as string);
  }

  stopPropagation(event: Event) {
    event.stopPropagation();
  }

  clearSelectedCategory(): void {
    this.selectedCategory = {
      Id: undefined,
      Description: "",
      Icon: "http://www.freeiconspng.com/uploads/pictures-icon-11.gif",
      Name: "",
      Tags: "",
      Subcategories: []
    };
  }

  clearSelectedSubcategory(): void {
    this.selectedSubcategory = {
      Id: null,
      Description: "",
      Icon: "http://www.freeiconspng.com/uploads/pictures-icon-11.gif",
      Name: "",
      Tags: "",
      Category: "",
      CategoryId: null
    };
  }

  isCategoryOnPending(category: Category): boolean {
    return this.pendingCategories.includes(category);
  }

  isSubcategoryOnPending(subcategory: Subcategory): boolean {
    return this.pendingSubcategories.includes(subcategory);
  }

  isCategoryValid(): boolean {
    return this.selectedCategory.Name !== "";
  }
  
  isSubcategoryValid(): boolean {
    return this.selectedSubcategory.Name !== "" && this.selectedSubcategory.CategoryId !== null;
  }

  closeNewSubcategoryEdit(): void {
    if (this.isNewSubcategoryEditOpen)
      this.isNewSubcategoryEditOpen = false;
  }

  onCategoryPanelOpen() {
    this.clearSelectedCategory();
    this.clearSelectedSubcategory();
  }
}
