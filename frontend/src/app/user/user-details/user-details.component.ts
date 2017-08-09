import { Component, OnInit,Input  } from '@angular/core';
import {SuiModule} from 'ng2-semantic-ui';
import { ActivatedRoute, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { User } from '../../models/user';

import { UserService } from "../../services/user.service";


@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  fakeUser:User;
  constructor( private route: ActivatedRoute,private userService: UserService) { 
  }
  ngOnInit() {
    this.fakeUser = this.userService.getUser(0);
  }

}
