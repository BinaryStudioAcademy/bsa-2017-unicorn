import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

// Routing
import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';
import { CategoryComponent } from './category/category.component';
import { VendorComponent } from './vendor/vendor.component';
import { UserComponent } from './user/user.component';
import { IndexComponent } from './index/index.component';
import { CategoryDetailsComponent } from './category-details/category-details.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { VendorDetailsComponent } from './vendor-details/vendor-details.component';

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    VendorComponent,
    UserComponent,
    IndexComponent,
    CategoryDetailsComponent,
    UserDetailsComponent,
    VendorDetailsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
