import { Component, OnInit } from '@angular/core';

import { MenuItem } from './menu-item/menu-item';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  items: MenuItem[];
  isEnabled: boolean;
  constructor() { }

  ngOnInit() {
    this.addMenuItems();
    this.isEnabled = false;
  }

  openModal() {
    this.isEnabled = true;
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
