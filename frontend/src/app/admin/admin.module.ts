import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './admin/admin.component';
import { BanListComponent } from './admin/ban-list/ban-list.component';
import { CategoriesComponent } from './admin/categories/categories.component';
import { AdminRoutingModule } from "./admin-routing.module";
import { SuiModule } from "ng2-semantic-ui/dist";

@NgModule({
  imports: [
    CommonModule,
    SuiModule,
    AdminRoutingModule
  ],
  declarations: [AdminComponent, BanListComponent, CategoriesComponent]
})
export class AdminModule { }
