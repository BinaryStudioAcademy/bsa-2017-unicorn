import { Component, OnInit } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit {
  location: string;

  constructor(private router: Router) {
  }

  ngOnInit() {
    this.getCurrentUrl();
    console.log(this.location);
  }

  getCurrentUrl() {
    this.router.events
    .subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.location = event.url;
      }
    });
  }

}
