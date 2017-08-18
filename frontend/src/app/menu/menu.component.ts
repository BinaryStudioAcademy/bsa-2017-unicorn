import { Component, OnInit } from '@angular/core';

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
  styleUrls: ['./menu.component.css'],
  providers: []
})
export class MenuComponent implements OnInit {
  items: MenuItem[];
  isEnabled: boolean;
  isLogged: boolean;

  onLogIn: Subscription;
  onLogOut: Subscription;

  constructor(
    private modalService: SuiModalService,
    private authEventService: AuthenticationEventService,
    private authLoginService: AuthenticationLoginService,
    private tokenHelper: TokenHelperService) {
    this.isLogged = this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired();
  }

  ngOnInit() {
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
  }
}
