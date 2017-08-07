import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

// Routing
import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';
import { IndexComponent } from './index/index.component';

import { CategoryModule } from './category/category.module';
import { UserModule } from './user/user.module';
import { VendorModule } from './vendor/vendor.module';

@NgModule({
  declarations: [
    AppComponent,
    IndexComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    CategoryModule,
    UserModule,
    VendorModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
