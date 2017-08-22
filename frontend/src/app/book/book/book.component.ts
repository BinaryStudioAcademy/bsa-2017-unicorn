import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  @ViewChild('bookForm') public bookForm: NgForm;

  constructor(private route: ActivatedRoute, private bookOrderService: BookOrderService, private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    this.book = {
      date: null,
      location: "",// TODO: get user location   
      description: "",
      workid: 0, // TODO: selected work from dropdown
      profile: this.getProfilePage(),
      profileid: +this.route.snapshot.paramMap.get('id'),
      customerid: +this.tokenHelper.getClaimByName('profileid')
    }    
  }

  makeOrder() {
    if (this.bookForm.invalid) {
      return;
    }
    this.order();
  }

  private order() {
    this.bookOrderService.createOrder(this.book)
      .then(x => alert('DONE!'))
      .catch(err => {
        alert('Error!!!');
        console.log(err);
      });
  }

  private getProfilePage(): string {
    return this.route.root.snapshot.firstChild.url[0].path; // vendor or company
  }
}
