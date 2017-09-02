import { Component, OnInit } from '@angular/core';
import { DbcreationService } from '../../services/helper/dbcreation.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-db',
  templateUrl: './db.component.html',
  styleUrls: ['./db.component.css']
})
export class DbComponent implements OnInit {

  constructor(private dbCreater: DbcreationService) { }

  ngOnInit() {
    this.dbCreater.RecreateDatabase().then(x => {
      localStorage.removeItem('token');
      location.href = 'index';
    }).catch(err => {
      console.log('err', err);
    });
  }

}
