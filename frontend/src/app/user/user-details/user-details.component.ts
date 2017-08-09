import { Component, OnInit,Input  } from '@angular/core';
import {SuiModule} from 'ng2-semantic-ui';
import { ActivatedRoute, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  constructor( private route: ActivatedRoute) { 
  }
  ngOnInit() {
  }

}
