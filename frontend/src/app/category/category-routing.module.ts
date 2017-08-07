import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CategoryComponent } from './category-component/category.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: "category", component: CategoryComponent },
      { path: "category/:id", component: CategoryDetailsComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class CategoryRoutingModule { }
