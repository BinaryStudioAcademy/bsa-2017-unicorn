import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

// Routing
import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';

// Modules
import { CategoryModule } from './category/category.module';
import { CompanyModule } from './company/company.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { LoginModule } from './login/login.module';
import { RegisterModule } from './register/register.module';
import { UserModule } from './user/user.module';
import { VendorModule } from './vendor/vendor.module';

import { IndexModule } from './index/index.module';
import { ShellComponent } from './shell/shell.component';
import { MenuComponent } from './menu/menu.component';

@NgModule({
  declarations: [
    AppComponent,
    ShellComponent,
    MenuComponent    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CategoryModule,
    CompanyModule,
    DashboardModule,
    LoginModule,
    RegisterModule,
    UserModule,
    VendorModule,
    IndexModule // // This fucking broke all routes, if it goes first
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
