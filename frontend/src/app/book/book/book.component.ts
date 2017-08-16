import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';

import { Book } from '../../models/book.model';

@Component({
  selector: 'app-book',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.sass']
})
export class BookComponent implements OnInit {
  book: Book;

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

}
