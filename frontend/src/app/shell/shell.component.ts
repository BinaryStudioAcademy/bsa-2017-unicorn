import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit {
  search: boolean;

  constructor() { }

  ngOnInit() {
    this.showSearch();
  }

  showSearch() {
    this.search = true;
  }

  hideSearch() {
    this.search = false;
  }

}
