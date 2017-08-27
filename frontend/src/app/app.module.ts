import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
// Enviroment
import { environment } from '../environments/environment.prod';
import { AngularFireModule } from 'angularfire2';
import { AngularFireAuthModule } from 'angularfire2/auth';

import { AuthenticationLoginService } from './services/auth/authenticationlogin.service';
import { AuthenticationEventService } from './services/events/authenticationevent.service';

import { HelperService } from './services/helper/helper.service';
import { ModalService } from "./services/modal/modal.service";

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
import { SignBlockModule } from './sign-block/sign-block.module';
import { IndexModule } from './index/index.module';
import { SearchModule } from './search/search.module';


import { SignalR, SignalRConnection, SignalRModule, SignalRConfiguration } from 'ng2-signalr';
import { SuiModule } from 'ng2-semantic-ui';
import {ToastOptions} from 'ng2-toastr';
import { ShellComponent } from './shell/shell.component';
import { MenuComponent } from './menu/menu.component';
import { FooterComponent } from './footer/footer.component';

import { RegisterComponent } from './register/register-component/register.component';
import { TokenHelperService } from './services/helper/tokenhelper.service';
import {ToastModule, Toast} from 'ng2-toastr/ng2-toastr';
import { AccountService } from "./services/account.service";

export function createConfig(): SignalRConfiguration {
  const c = new SignalRConfiguration();
  c.hubName = 'NotificationHub';
  // c.url = "http://localhost:52309";
  c.logging = true;
  return c;
}
export class CustomOptions extends ToastOptions {
  animate = 'fade';
  dismiss = 'auto';
  showCloseButton = true;
  newestOnTop = true;
  enableHTML = true;
  positionClass = 'toast-bottom-right';
}
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
    BrowserAnimationsModule, 
    ToastModule.forRoot(),
    HttpClientModule,
    AppRoutingModule,
    SignBlockModule,
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
    SignalRModule.forRoot(createConfig),
    IndexModule // Must be the last module
  ],
  providers: [
    AuthenticationLoginService,
    AuthenticationEventService,
    HelperService,
    TokenHelperService,
    AccountService,
   { provide: ToastOptions, useClass: CustomOptions}
  ],
  entryComponents: [RegisterComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
