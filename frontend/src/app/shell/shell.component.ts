import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit, OnDestroy {
  location: string;
  private subscription: ISubscription;

  constructor(private router: Router) {
  }

  ngOnInit() {
    this.getCurrentUrl();
  }

  getCurrentUrl() {
    this.subscription = this.router.events
    .subscribe((event) => {
      if (event instanceof NavigationStart) {
        this.location = event.url;
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
