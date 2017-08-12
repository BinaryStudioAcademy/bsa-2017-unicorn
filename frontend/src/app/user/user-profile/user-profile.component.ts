import { Component, OnInit, Input } from '@angular/core';
import { NgModel } from '@angular/forms';
import { User } from '../../models/user';
@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.sass']
})
export class UserProfileComponent implements OnInit {

  
  @Input() user: User;
  constructor() { }

  ngOnInit() {
  }

}
