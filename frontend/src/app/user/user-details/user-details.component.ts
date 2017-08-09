import { Component, OnInit } from '@angular/core';
import {SuiModule} from 'ng2-semantic-ui';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  constructor() { 
  }
   name:string='Dan';
   surname:string='Brown'
  ngOnInit() {
  }

}
