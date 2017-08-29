import { Component } from '@angular/core';
import { DataService } from './services/data.service';
import {  OnInit, ViewContainerRef } from '@angular/core';
import { ToastsManager, ToastOptions } from 'ng2-toastr/ng2-toastr';

@Component({
  selector: 'app-root',
  providers: [DataService],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title: string;
  data: any;

  constructor(private dataService: DataService, public toastr: ToastsManager, vRef: ViewContainerRef) {
    this.title = 'app';
    this.toastr.setRootViewContainerRef(vRef);
  }
}
