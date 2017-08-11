import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';

import { ISubscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit, OnDestroy {
  searchVisible: boolean;
  private subscription: ISubscription;

  constructor(private router: Router) {
  }

  ngOnInit() {
    this.getSearchVisibility();
  }

  getSearchVisibility() {
    this.subscription = this.router.events
    .subscribe((event) => {
      if (event instanceof NavigationStart) {
        if (event.url !== '/index' && event.url !== '/') {
          this.searchVisible = true;
        } else {
          this.searchVisible = false;
        }
      }
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

}
