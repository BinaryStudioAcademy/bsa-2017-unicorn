import { Component, OnInit, ViewChild, AfterViewChecked, Input } from '@angular/core';
import { AgmMap } from "@agm/core";

export interface IContext {
    data:string;
}

@Component({
  selector: 'company-contacts',
  templateUrl: './contacts.component.html',
  styleUrls: ['./contacts.component.sass']
})
export class ContactsComponent implements OnInit, AfterViewChecked {  
  @ViewChild(AgmMap) private map: any;
  lat: number = 49.85711;
  lng: number = 24.01980;  


  constructor() { }

  ngOnInit() {     
  }    


  ngAfterViewChecked() {      
    this.redrawMap();     
  }

  
  private redrawMap() {    
    this.map.triggerResize();
      //  .then(() => this.map._mapsWrapper.setCenter({lat: this.lat, lng: this.lng}));
  }  
}
