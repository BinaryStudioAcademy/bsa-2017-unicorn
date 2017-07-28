import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title: string;
  data: any;

  constructor(private http: HttpClient) {
    this.title = 'app';

    http.get('http://localhost:52309/api/values').subscribe((data) => this.data = data);
  }
}
