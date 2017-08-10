import { Component, OnInit, Input } from '@angular/core';
import { NgModel } from '@angular/forms';
import { User } from '../../models/user';
@Component({
  selector: 'app-user-history',
  templateUrl: './user-history.component.html',
  styleUrls: ['./user-history.component.sass']
})
export class UserHistoryComponent implements OnInit {

  constructor() { }
  @Input() user: User;
  ngOnInit() {
  }

}
