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
import { ChatEventsService } from "./services/events/chat-events.service";
import { MenuEventsService } from "./services/events/menu-events.service";

import { HelperService } from './services/helper/helper.service';
import { ModalService } from "./services/modal/modal.service";

// Routing
import { AppRoutingModule } from './app-routing/app-routing.module';

import { AppComponent } from './app.component';

// Modules
import { CategoryModule } from './category/category.module';
import { CompanyModule } from './company/company.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { DbModule } from './db/db.module';
import { RegisterModule } from './register/register.module';
import { UserModule } from './user/user.module';
import { VendorModule } from './vendor/vendor.module';
import { BookModule } from './book/book.module';
import { SignBlockModule } from './sign-block/sign-block.module';
import { IndexModule } from './index/index.module';
import { SearchModule } from './search/search.module';
import { PipeModule } from "./pipe/pipe.module";
import { MomentModule } from 'angular2-moment';

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
import { NotificationService } from "./services/notifications/notification.service";

import { ChatModule } from "./chat/chat.module";
import { ReviewModalComponent } from './review/review-modal/review-modal.component';
import { ChatLogicService } from "./services/chat/chat-logic.service";
import { CalendarService } from "./services/calendar-service";
import { ClickOutsideModule } from "ng-click-outside/lib";
import { NotFoundModule} from './not-found/not-found.module'

export class CustomOptions extends ToastOptions {
  animate = 'fade';
  dismiss = 'auto';
  showCloseButton = true;
  newestOnTop = true;
  enableHTML = true;
  positionClass = 'toast-bottom-right';
}

export function getDefaultSignalRConfig(): SignalRConfiguration {
  const config = new SignalRConfiguration();
  config.logging = true;
  return config;
}

@NgModule({
  declarations: [
    AppComponent,
    ShellComponent,
    MenuComponent,
    FooterComponent,
    ReviewModalComponent
  ],
  imports: [
    SuiModule,
    ChatModule,
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
    DbModule,
    NotFoundModule,
    RegisterModule,
    UserModule,
    VendorModule,
    BookModule,
    SearchModule,
    SignalRModule.forRoot(getDefaultSignalRConfig),
    MomentModule,
    PipeModule,
    ClickOutsideModule,
    IndexModule // Must be the last module
  ],
  providers: [
    AuthenticationLoginService,
    AuthenticationEventService,
    ChatEventsService,
    MenuEventsService,
    CalendarService,
    HelperService,
    TokenHelperService,
    AccountService,
    NotificationService,
    ChatLogicService,
   { provide: ToastOptions, useClass: CustomOptions}
  ],
  entryComponents: [
    RegisterComponent,
    ReviewModalComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
