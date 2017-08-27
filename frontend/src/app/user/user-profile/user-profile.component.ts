import { Component, OnInit, Input, ViewChild, AfterViewChecked, ViewContainerRef } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';
import { User } from '../../models/user';
import { AgmMap} from "@agm/core";
import { NguiMapModule, Marker } from "@ngui/map";
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
    @ViewChild(AgmMap) private map: any;
      location: Location;

    @ViewChild('userForm') public userForm: NgForm;
    birthday: Date;
    dataLoaded: boolean;

    constructor(private userService: UserService, 
        public toastr: ToastsManager) {}
  
    ngOnInit() { 
        this.dataLoaded=true;
    }

    markerDragged($event)
    {
        this.user.Location.Latitude = $event.coords.lat;
        this.user.Location.Longitude = $event.coords.lng;
        this.getLocDetails()
        .subscribe(
        result => {
           
            this.user.Location.Adress=result.formatted_address;
            this.user.Location.City=result.address_components[3].short_name;
        },
        error => console.log(error),
        () => console.log('Geocoding completed!')
        );
    }
    getLocDetails()
    {
        let geocoder = new google.maps.Geocoder();
        var latlng = {lat: this.user.Location.Latitude, lng: this.user.Location.Longitude}
        return Observable.create(observer => {
            geocoder.geocode( { 'location':latlng }, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    observer.next(results[0]);
                    observer.complete();
                } else {
                    console.log('Error - ', results, ' & Status - ', status);
                    observer.next({});
                    observer.complete();
                }
            });
        })
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

