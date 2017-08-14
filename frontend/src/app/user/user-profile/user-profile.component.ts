import { Component, OnInit, Input, ViewChild, AfterViewChecked } from '@angular/core';
import { NgModel } from '@angular/forms';
import { User } from '../../models/user';
import { AgmMap } from "@agm/core";
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
    lat: number = 49.85711;
    lng: number = 24.01980;

    // constructor() { }
    //  mapClicked($event: MouseEvent) {
    //  this.lat = $event.coords.lat;
    //  this.lng = $event.coords.lng;
    //}
    ngOnInit() {
    }
}
