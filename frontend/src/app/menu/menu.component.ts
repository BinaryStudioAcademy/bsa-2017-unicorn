import { Component, OnInit } from '@angular/core';

import { MenuItem } from './menu-item/menu-item';

import { SuiModalService, TemplateModalConfig, SuiModal, ComponentModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import { ConfirmModal, IConfirmModalContext } from '../register/register-component/register.component';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  items: MenuItem[];
  isEnabled: boolean;
  public activeModal: SuiActiveModal<IConfirmModalContext, {}, void>;
  constructor(private modalService: SuiModalService) { }

  ngOnInit() {
    this.addMenuItems();
    this.isEnabled = true;
  }

  openModal() {
    this.activeModal = this.modalService
    .open(new ConfirmModal("Are you sure?", "Are you sure about accepting this?"))
    .onApprove(() => alert("User has accepted."))
    .onDeny(() => (''));
  }

  addMenuItems() {
    this.items = [{
      name: 'Get task',
      route: '#'
    }, {
      name: 'Vendors',
      route: '#'
    }, {
      name: 'Registration',
      route: 'register'
    }, {
      name: 'Log in',
      route: '#'
    }];
  }
}
