import { Component, OnInit } from '@angular/core';
import { AdminAuthService } from "../../services/admin-auth.service";
import { ActivatedRoute } from "@angular/router";
import { SuiDimmerModule } from "ng2-semantic-ui";

@Component({
  selector: 'app-switch',
  templateUrl: './switch.component.html',
  styleUrls: ['./switch.component.css']
})
export class SwitchComponent implements OnInit {

  constructor(private adminService: AdminAuthService, private route: ActivatedRoute) { }

  ngOnInit() {
    const accoundId = +this.route.snapshot.paramMap.get('id');
    this.adminService.switchAccount(accoundId).then(x => {
      localStorage.setItem('token', (x.headers.get('token')));
      location.href = 'index';      
    })
      .catch(err => console.log(err))
  }

}
