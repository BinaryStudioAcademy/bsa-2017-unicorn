import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { CategoryComponent } from './category-component/category.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'categories',
        component: CategoryComponent,
      }
    ]),
    RouterModule.forChild([
      {
        path: 'category',
        children: [
          {
            path: '',
            redirectTo: '/categories',
            pathMatch: 'full'
          },
          {
            path: ':id',
            component: CategoryDetailsComponent,
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class CategoryRoutingModule { }
