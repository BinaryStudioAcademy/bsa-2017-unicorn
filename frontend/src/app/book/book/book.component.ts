import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';

import { SuiModule } from 'ng2-semantic-ui';
import { BookOrderService } from '../../services/book-order.service';
import { UserService } from '../../services/user.service';
import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { BookOrder } from '../../models/book/book-order';
import { LocationModel } from '../../models/location.model';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.sass']
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

  constructor(private bookOrderService: BookOrderService, private tokenHelper: TokenHelperService, private userService: UserService) { }

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

    this.book = {
      date: new Date(),
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

  onWorkChange(){
    this.book.workid = this.selectedWork.Id;
  }

  makeOrder() {
    if (this.bookForm.invalid) {
      return;
    }
    this.order();
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