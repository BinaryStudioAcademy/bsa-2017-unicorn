import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../../models/user';

@Component({
  selector: 'app-user-main-info',
  templateUrl: './user-main-info.component.html',
  styleUrls: ['./user-main-info.component.css']
})
export class UserMainInfoComponent implements OnInit {

  @Input() user: User;
  constructor() { }
  ngOnInit() {
  }

}
