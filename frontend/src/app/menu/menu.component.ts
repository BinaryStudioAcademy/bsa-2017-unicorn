import { Component, OnInit } from '@angular/core';

import { MenuItem } from './menu-item/menu-item';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  items: MenuItem[];

  constructor() { }

  ngOnInit() {
    this.addMenuItems();
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
