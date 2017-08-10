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
      name: 'Взяться и сделать',
      route: '#'
    }, {
      name: 'Помощь',
      route: '#'
    }, {
      name: 'Регистрация',
      route: 'register'
    }, {
      name: 'Войти',
      route: '#'
    }];
  }
}
