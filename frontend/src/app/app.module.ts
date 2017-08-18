import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

// Enviroment
import { environment } from '../environments/environment.prod';
import { AngularFireModule } from 'angularfire2';
import { AngularFireAuthModule } from 'angularfire2/auth';

import { AuthenticationLoginService } from './services/auth/authenticationlogin.service';
import { AuthenticationEventService } from './services/events/authenticationevent.service';

import { HelperService } from './services/helper/helper.service';

// Routing
import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';

// Modules
import { CategoryModule } from './category/category.module';
import { CompanyModule } from './company/company.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { RegisterModule } from './register/register.module';
import { UserModule } from './user/user.module';
import { VendorModule } from './vendor/vendor.module';
import { BookModule } from './book/book.module';
import { IndexModule } from './index/index.module';
// import { ImageUploadModule } from "angular2-image-upload";
import { SearchModule } from './search/search.module';



import { SuiModule } from 'ng2-semantic-ui';

import { ShellComponent } from './shell/shell.component';
import { MenuComponent } from './menu/menu.component';
import { FooterComponent } from './footer/footer.component';

import { RegisterComponent } from './register/register-component/register.component';
import { TokenHelperService } from './services/helper/tokenhelper.service';

@NgModule({
  declarations: [
    AppComponent,
    ShellComponent,
    MenuComponent,
    FooterComponent
  ],
  imports: [
    SuiModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    AngularFireModule.initializeApp(environment.firebase, 'unicorn-angular'),
    AngularFireAuthModule,
    CategoryModule,
    CompanyModule,
    DashboardModule,
    RegisterModule,
    UserModule,
    VendorModule,
    BookModule,
    SearchModule,
    IndexModule // Must be the last module
  ],
  providers: [
    AuthenticationLoginService,
    AuthenticationEventService,
    HelperService,
    TokenHelperService
  ],
  entryComponents: [RegisterComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
