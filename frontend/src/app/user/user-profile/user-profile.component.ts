import { Component, OnInit, Input, ViewChild, AfterViewChecked } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';
import { User } from '../../models/user';
import { AgmMap } from "@agm/core";
import { NguiMapModule, Marker } from "@ngui/map";
import { LocationService } from "../../services/location.service";
import { UserService } from "../../services/user.service";

export interface IContext {
    data: string;
}
@Component({
    selector: 'app-user-profile',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.sass']
})


export class UserProfileComponent implements OnInit {

    @Input() user: User;
    @ViewChild(AgmMap) private map: any;
      location: Location;

    @ViewChild('userForm') public userForm: NgForm;
    lat: number = 48.464921;
    lng: number = 35.045798;
    birthday: Date;
    dataLoaded: boolean;

    constructor(private userService: UserService, private locationService : LocationService) { }
   mapClicked($event: MouseEvent){
      this.lat=$event.clientX;
      this.lng=$event.clientY;
    
  }
    ngOnInit() { 
    }

    updateUser(): void {
        if (!this.userForm.valid) {
            return;
        }
        this.user.Birthday=this.birthday;
        this.user.Birthday.setDate( this.user.Birthday.getDate()+1);
        this.userService.updateUser(this.user)
          .then(resp => this.user = resp.body as User);
      }
}

