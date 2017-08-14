import { Component, OnInit,Input,OnDestroy  } from '@angular/core';
import { FormsModule }   from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';
import { ActivatedRoute, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { User } from '../../models/user';

import { UserService } from "../../services/user.service";
import { ImageUploadModule } from "angular2-image-upload";

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.sass']
})
export class UserDetailsComponent implements OnInit {
  
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton:boolean = false;
  fakeUser:User;
  constructor( private route: ActivatedRoute,private userService: UserService) { 
  }
  ngOnInit() {
    this.fakeUser = this.userService.getUser(0);
  }
 openModal() {
    this.enabled = true;
  }
 updateBg(color:string)
 {
    document.getElementById("user-header").style.backgroundColor = color;
 }
}