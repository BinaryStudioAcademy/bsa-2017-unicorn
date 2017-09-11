import { Component, OnInit, ViewChild, Input, ChangeDetectorRef } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';

import { SuiModule } from 'ng2-semantic-ui';
import { BookOrderService } from '../../services/book-order.service';
import { LocationService } from "../../services/location.service";
import { UserService } from '../../services/user.service';
import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { BookOrder } from '../../models/book/book-order';
import { LocationModel } from '../../models/location.model';
import { NgMapAsyncApiLoader } from "@ngui/map/dist";

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.sass'],
  providers: [
    NgMapAsyncApiLoader
  ]
})
export class BookComponent implements OnInit {
  book: BookOrder;
  formIsSended: boolean;
  onSending: boolean;
  private defaultLocation: LocationModel;
  private selectedWork: any;

  @Input() routePath: string;
  @Input() routeId: number;
  @Input() works: any;

  @ViewChild('bookForm') public bookForm: NgForm;

  location: LocationModel;

  constructor(
    private bookOrderService: BookOrderService, 
    private tokenHelper: TokenHelperService, 
    private userService: UserService,
    public LocationService: LocationService,
    private apiLoader: NgMapAsyncApiLoader,
    private ref: ChangeDetectorRef
  ) { }

  ngOnInit() {
    this.formIsSended = false;
    this.onSending = true;

    this.defaultLocation = {
      Id: 0,
      City: "",
      Adress: "",
      PostIndex: "",
      Latitude: 0,
      Longitude: 0
    }

    this.location = {
      Id: 0,
      City: "",
      Adress: "",
      PostIndex: "",
      Latitude: 0,
      Longitude: 0
    }

    this.book = {
      date: new Date(),
      endDate: null,
      location: this.defaultLocation,
      description: "",
      workid: 0,
      profile: this.routePath,
      profileid: this.routeId,
      customerid: +this.tokenHelper.getClaimByName('profileid'),
      customerphone: ""
    }

    this.getUserData();
  }

  initLocation() {
    this.apiLoader.load();
    this.LocationService.getGoogle().then((g) => {
      this.LocationService.getLocDetails(this.location.Latitude, this.location.Longitude).subscribe(
        result => {
          this.location.Adress=(result.address_components[1].short_name+','+result.address_components[0].short_name)
          this.location.City=result.address_components[3].short_name;
        }
      );
    });
  }

  placeChanged(event) {
    
    this.location.Latitude = event.geometry.location.lat();
    this.location.Longitude = event.geometry.location.lng()
    this.ref.detectChanges();
    this.LocationService.getLocDetails(this.location.Latitude,this.location.Longitude)
    .subscribe(
     result => {    
        this.location.Adress=(result.address_components[1].short_name+','+result.address_components[0].short_name)
         this.location.City=result.address_components[3].short_name;});
  }

  onWorkChange() {
    if (this.selectedWork != undefined) {
      this.book.workid = this.selectedWork.Id;
    }
  }

  selectWork(work) {
    this.selectedWork = work;
    this.onWorkChange();
  }

  makeOrder() {
    if (this.bookForm.invalid) {
      return;
    }
    this.validateEndDate();
    this.order();
  }

  private validateEndDate() {
    let date = this.book.date;
    let endDate = this.book.endDate;
    if (endDate === null || endDate === undefined || endDate.getTime() < date.getTime()) {
      this.book.endDate = new Date(date); 
    }
  }

  private updateLoader() {
    this.onSending = !this.onSending;
  }

  private order() {
    this.updateLoader();
    this.bookOrderService.createOrder(this.book)
      .then(x => {
        this.updateLoader();
        this.formIsSended = true;
      })
      .catch(err => {
        this.updateLoader();
        console.log(err);
      });
  }

  private getUserData() {
    this.userService.getUserForOrder(this.book.customerid)
      .then(user => {
        this.book.location = user.Location;
        this.book.customerphone = user.Phone;
        this.updateLoader();
      })
      .catch(err => {
        this.updateLoader();
        console.log(err);
      });
  }

  private adressChanged(event) {
    this.book.location.Id = -1;
  }
}