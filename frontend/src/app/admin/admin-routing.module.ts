import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AuthGuard } from '../guards/auth.guard';
import { AdminComponent } from "./admin/admin.component";
import { BanListComponent } from "./admin/ban-list/ban-list.component";
import { CategoriesComponent } from "./admin/categories/categories.component";
import { FeedbackComponent } from "./admin/feedback/feedback.component";

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'admin',
        component: AdminComponent,
        children: [
          {
            path: 'ban-list',
            component: BanListComponent
          },
          {
            path: 'categories',
            component: CategoriesComponent,
          },
          {
            path: 'feedback',
            component: FeedbackComponent,
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ],
  providers: [AuthGuard]
})
export class AdminRoutingModule { }
