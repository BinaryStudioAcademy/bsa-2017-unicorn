import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-not-found',
  templateUrl: './not-found.component.html',
  styleUrls: ['./not-found.component.sass']
})
export class NotFoundComponent implements OnInit {

  message:string ="";
  constructor(private route: ActivatedRoute) { this.message = this.route.snapshot.queryParams['message'];}
  ngOnInit() {
  }

}
