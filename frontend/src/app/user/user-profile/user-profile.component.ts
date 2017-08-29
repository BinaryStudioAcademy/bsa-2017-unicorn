import { Component, OnInit, Input, ViewChild, AfterViewChecked, ViewContainerRef, ChangeDetectorRef } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';
import { User } from '../../models/user';
import { LocationService } from "../../services/location.service";
import { UserService } from "../../services/user.service";
import {ToastsManager, Toast} from 'ng2-toastr';
import {ToastOptions} from 'ng2-toastr';
import { Observable, Observer } from 'rxjs';
import { NguiMapModule, Marker } from "@ngui/map";

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
    position;
    autocomplete: google.maps.places.Autocomplete;
    address: any = {};
    marker;
    constructor(private userService: UserService, 
        public toastr: ToastsManager, public LocationService: LocationService,
        private ref: ChangeDetectorRef) {
          
        }
  
    ngOnInit() { 
        this.position={lat: this.user.Location.Latitude, lng: this.user.Location.Longitude};
        this.dataLoaded=true;
    }

    markerDragged(event)
    {
         this.user.Location.Latitude = event.latLng.lat();
         this.user.Location.Longitude = event.latLng.lng()

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

      initialized(autocomplete: any) {
        this.autocomplete = autocomplete;
      }
    
      placeChanged(event) {
        let place = this.autocomplete.getPlace();
        for (let i = 0; i < place.address_components.length; i++) {
          let addressType = place.address_components[i].types[0];
          this.address[addressType] = place.address_components[i].long_name;
        }
    
        this.position = this.address.locality;
        this.user.Location.Latitude = event.geometry.location.lat();
        this.user.Location.Longitude = event.geometry.location.lng()

        this.LocationService.getLocDetails(this.user.Location.Latitude,this.user.Location.Longitude)
       .subscribe(
        result => {
          
           this.user.Location.Adress=result.formatted_address;
            this.user.Location.City=result.address_components[3].short_name;
        },
        error => console.log(error),
        () => console.log('Geocoding completed!')
        );
        this.ref.detectChanges();
      }
}

