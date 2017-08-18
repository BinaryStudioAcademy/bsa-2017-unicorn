import { Component, OnInit } from '@angular/core';

import { MenuItem } from './menu-item/menu-item';

import { LoginService } from '../services/events/login.service';
import { Subscription } from 'rxjs/Subscription';

import {
  SuiModalService, TemplateModalConfig, SuiModal, ComponentModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal
} from 'ng2-semantic-ui';
import { ConfirmModal, IConfirmModalContext } from '../register/register-component/register.component';
import { AuthService } from '../services/auth/auth.service';

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
  subscription: Subscription;

  constructor(private modalService: SuiModalService, private eventLoginService: LoginService, private authService: AuthService) {
    this.isLogged = localStorage.getItem('token') != null;
  }

  ngOnInit() {
    this.addMenuItems();
    this.isEnabled = true;

    this.subscription = this.eventLoginService.loginEvent$
      .subscribe(x => {
        this.isLogged = x;
      });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  openModal() {
    this.modalService
      .open(new ConfirmModal("Are you sure?", "Are you sure about accepting this?"))
      .onApprove(() => alert("User has accepted."))
      .onDeny(() => (''));
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
    this.authService.logout();
    this.isLogged = false;
  }
}
