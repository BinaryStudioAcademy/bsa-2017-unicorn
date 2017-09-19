import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { ClickOutsideModule } from 'ng-click-outside';
import { SuiModule } from "ng2-semantic-ui/dist";

import { AdminRoutingModule } from "./admin-routing.module";

import { AdminComponent } from './admin/admin.component';
import { BanListComponent } from './admin/ban-list/ban-list.component';
import { CategoriesComponent } from './admin/categories/categories.component';
import { FeedbackComponent } from './admin/feedback/feedback.component';
import { AuthModalComponent } from './admin/auth-modal/auth-modal.component';

import { AccountService } from "../services/account.service";
import { ReportService } from "../services/report.service";
import { CategoryService } from "../services/category.service";
import { AdminAuthService } from "../services/admin-auth.service";
import { ChatModule } from '../chat/chat.module';
import { MessagesComponent } from './admin/messages/messages.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SuiModule,
    AdminRoutingModule,
    ClickOutsideModule,
    ChatModule
  ],
  declarations: [
    AdminComponent,
    BanListComponent, 
    CategoriesComponent, 
    FeedbackComponent, 
    AuthModalComponent, 
    MessagesComponent
  ],
  providers: [
    AccountService,
    ReportService,
    CategoryService,
    AdminAuthService
  ],
  entryComponents: [
    AuthModalComponent
  ]
})
export class AdminModule { }
