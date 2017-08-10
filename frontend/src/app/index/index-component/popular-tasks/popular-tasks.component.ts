import { Component, OnInit } from '@angular/core';

@Component({
  // tslint:disable-next-line:component-selector
  selector: 'popular-tasks',
  templateUrl: './popular-tasks.component.html',
  styleUrls: ['./popular-tasks.component.css']
})
export class PopularTasksComponent implements OnInit {
  // tabs: {id: number, name: string}[];
  tabs: Tab[];

  constructor() { }

  ngOnInit() {
    this.createTabs();
  }

  createTabs() {
    this.tabs = [{
      id: 1,
      name: 'Popular'
    }, {
      id: 2,
      name: 'Cleaning',
    }, {
      id: 3,
      name: 'Building',
    }, {
      id: 4,
      name: 'Relocation',
    }, {
      id: 5,
      name: 'Сourier',
    }];
  }

}

export class Tab {
  id: number;
  name: string;
}
