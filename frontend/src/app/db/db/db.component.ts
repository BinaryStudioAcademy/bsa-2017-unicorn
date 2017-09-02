import { Component, OnInit } from '@angular/core';
import { DbcreationService } from '../../services/helper/dbcreation.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-db',
  templateUrl: './db.component.html',
  styleUrls: ['./db.component.css']
})
export class DbComponent implements OnInit {

  constructor(private router: Router, private dbCreater: DbcreationService) { }

  ngOnInit() {
    this.dbCreater.RecreateDatabase().then(x => {
      localStorage.removeItem('token');
      this.router.navigate(['index']);
    }).catch(err => {
      console.log('err', err);
    });
  }

}
