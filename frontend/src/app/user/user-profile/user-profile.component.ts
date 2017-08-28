import { Component, OnInit, Input, ViewChild, AfterViewChecked, ViewContainerRef } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';
import { User } from '../../models/user';
import { AgmMap} from "@agm/core";
import { LocationService } from "../../services/location.service";
import { UserService } from "../../services/user.service";
import {ToastsManager, Toast} from 'ng2-toastr';
import {ToastOptions} from 'ng2-toastr';
import { Observable, Observer } from 'rxjs';
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

    @ViewChild('userForm') public userForm: NgForm;
    birthday: Date;
    dataLoaded: boolean;

    constructor(private userService: UserService, 
        public toastr: ToastsManager, public LocationService: LocationService) {}
  
    ngOnInit() { 
        this.dataLoaded=true;
    }

    markerDragged($event)
    {
        this.user.Location.Latitude = $event.coords.lat;
        this.user.Location.Longitude = $event.coords.lng;
        this.LocationService.getLocDetails(this.user.Location.Latitude,this.user.Location.Longitude)
        .subscribe(
        result => {
           
            this.user.Location.Adress=result.formatted_address;
            this.user.Location.City=result.address_components[3].short_name;
        },
        error => console.log(error),
        () => console.log('Geocoding completed!')
        );
    }
   
    updateUser(): void {
        
        this.dataLoaded = false;
        if (!this.userForm.valid) {
            this.dataLoaded = true;
            this.toastr.error('Sorry, you must fill all inputs', 'Error!');
            return;
        }
        if(this.birthday!=undefined)
         { 
           this.user.Birthday=this.birthday;
           this.user.Birthday.setDate( this.user.Birthday.getDate()+1);
         }
        
        this.userService.updateUser(this.user)
          .then(resp => {this.user = resp.body as User;
            this.dataLoaded = true;
            this.toastr.success('Changes were saved', 'Success!');})
          .catch(x=>{ this.dataLoaded = true;
            this.toastr.error('Sorry, something went wrong', 'Error!');});
      }
}

