import { Component } from '@angular/core';
import { DataService } from './services/data.service';

@Component({
  selector: 'app-root',
  providers: [DataService],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent {
  title: string;
  data: any;

  constructor(private dataService: DataService) {
    this.title = 'app';

    dataService.getData().subscribe((data) => this.data = data);
  }
}
