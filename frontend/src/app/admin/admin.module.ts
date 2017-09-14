import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AdminComponent } from './admin/admin.component';
import { BanListComponent } from './admin/ban-list/ban-list.component';
import { CategoriesComponent } from './admin/categories/categories.component';
import { AdminRoutingModule } from "./admin-routing.module";
import { SuiModule } from "ng2-semantic-ui/dist";
import { AccountService } from "../services/account.service";
import { FeedbackComponent } from './admin/feedback/feedback.component';
import { ReportService } from "../services/report.service";
import { CategoryService } from "../services/category.service";

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SuiModule,
    AdminRoutingModule
  ],
  declarations: [
    AdminComponent,
    BanListComponent, 
    CategoriesComponent, FeedbackComponent
  ],
  providers: [
    AccountService,
    ReportService,
    CategoryService
  ]
})
export class AdminModule { }
