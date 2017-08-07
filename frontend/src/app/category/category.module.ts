import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryComponent } from './category-component/category.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';

import { CategoryRoutingModule } from './category-routing.module';

@NgModule({
  imports: [
    CommonModule,
    CategoryRoutingModule
  ],
  declarations: [
    CategoryComponent,
    CategoryDetailsComponent
  ]
})
export class CategoryModule { }
