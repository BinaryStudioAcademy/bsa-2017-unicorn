import { Component, OnInit, ViewChild } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';

import { Book } from '../../models/book.model';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.sass']
})
export class BookComponent implements OnInit {
  book: Book;

  @ViewChild('bookForm') public bookForm: NgForm;

  constructor() { }

  ngOnInit() {
    this.book = {
      date: null,
      address: "",
      contact: "",
      description: "",
      vendor: null,
      status: "",
      workType: ""
    }    
  }

  makeOrder() {
    if (this.bookForm.invalid) {
      return;
    }
    // do something ..
  }
}
