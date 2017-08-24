import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { MenuItem } from './menu-item/menu-item';

import { AuthenticationEventService } from '../services/events/authenticationevent.service';
import { AuthenticationLoginService } from '../services/auth/authenticationlogin.service';

import { Subscription } from 'rxjs/Subscription';
import { TokenHelperService } from '../services/helper/tokenhelper.service';

import { SuiModalService } from 'ng2-semantic-ui';
import { RegisterModal } from '../register/register-component/register.component';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.sass'],
  providers: []
})
export class MenuComponent implements OnInit {
  items: MenuItem[];
  isEnabled: boolean;
  isLogged: boolean;

  onLogIn: Subscription;
  onLogOut: Subscription;

  fakeName: string;
  fakeSurname: string;
  fakeEmail: string;
  showAccountDetails: boolean;
  showNotifications: boolean;
  notifications: Array<string>;

  constructor(
    private router: Router,
    private modalService: SuiModalService,
    private authEventService: AuthenticationEventService,
    private authLoginService: AuthenticationLoginService,
    private tokenHelper: TokenHelperService) {
    this.isLogged = this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired();
  }

  ngOnInit() {
    this.fakeName = "Name";
    this.fakeSurname = "Surname";
    this.fakeEmail = "balanykb@gmail.com";
    this.notifications = [
      "First notification",
      "Second notification",
      "Third notification"
    ];

    this.addMenuItems();
    this.isEnabled = true;

    this.onLogIn = this.authEventService.loginEvent$
      .subscribe(() => {
        this.isLogged = true;
      });

    this.onLogOut = this.authEventService.logoutEvent$
      .subscribe(() => {
        this.isLogged = false;
      });
  }

  ngOnDestroy() {
    this.onLogIn.unsubscribe();
    this.onLogOut.unsubscribe();
  }

  openModal() {
    this.modalService.open(new RegisterModal());
  }

  addMenuItems() {
    this.items = [{
      name: 'Search',
      route: 'search'
    }, {
      name: 'Vendors',
      route: '#'
    }, {
      name: 'Sign in',
      route: 'register'
    }];
  }

  signOut() {
    this.authLoginService.signOut();
    this.router.navigate(['index']);
  }

  goToAccount() {

  }

  onShowDetails() {
    this.showAccountDetails = !this.showAccountDetails;
    this.showNotifications = false;
  }

  onShowNotifications() {
    this.showNotifications = !this.showNotifications;
    this.showAccountDetails = false;
  }

  getNotificationClass() : string {
    return this.isNotificationExist() ? "red" : "";
  }

  isNotificationExist() : boolean {
    return this.notifications && this.notifications.length != 0;
  }
}
