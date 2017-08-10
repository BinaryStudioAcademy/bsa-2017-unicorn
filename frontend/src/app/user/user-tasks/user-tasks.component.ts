import { Component, OnInit, Input } from '@angular/core';
import { User } from '../../models/user';
import { NgModel } from '@angular/forms';
@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrls: ['./user-tasks.component.sass']
})
export class UserTasksComponent implements OnInit {

  
  @Input() user:User;
  constructor() { }

  ngOnInit() {
  }

}
