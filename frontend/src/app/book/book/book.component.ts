import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';
import { BookOrderService } from '../../services/book-order.service';
import { BookOrder } from '../../models/book/book-order';
import { TokenHelperService } from '../../services/helper/tokenhelper.service';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.sass']
})
export class BookComponent implements OnInit {
  book: BookOrder;
  formIsSended: boolean;
  onSending: boolean;

  @Input() routePath: string;
  @Input() routeId: number;

  @ViewChild('bookForm') public bookForm: NgForm;

  constructor(private bookOrderService: BookOrderService, private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    this.formIsSended = false;
    this.book = {
      date: null,
      location: "",// TODO: get user location   
      description: "",
      workid: 0, // TODO: selected work from dropdown
      profile: this.routePath,
      profileid: this.routeId,
      customerid: +this.tokenHelper.getClaimByName('profileid'),
      phone: ""
    }    
  }

  makeOrder() {
    if (this.bookForm.invalid) {
      return;
    }
    this.order();
  }

  private order() {
    this.onSending = !this.onSending;
    this.bookOrderService.createOrder(this.book)
      .then(x => {
        this.onSending = !this.onSending;
        this.formIsSended = true;
        alert('DONE');
      })
      .catch(err => {
        this.onSending = !this.onSending;
        alert('Error!!!');
        console.log(err);
      });
  }
}
