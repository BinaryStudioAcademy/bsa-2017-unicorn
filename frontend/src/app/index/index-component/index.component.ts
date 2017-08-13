import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  title: string;
  slogan: string;
  labelSearch: string;
  placeholderSearch: string;
  searchText: string;
  labelDate: string;
  searchDate: Date;
  mode: string;
  firstDayOfWeek: string;
  labelCategory: string;
  labelSubcategory: string;
  categories: string[];
  selCategory: string;
  subcategories: string[];
  selSubcategory: string;
  labelButton: string;

  constructor() { }

  ngOnInit() {
    this.initContent();
  }

  initContent() {
    this.title = 'Happy unicorn';
    this.slogan = 'We can scratch your cat right now, because we can do it.';

    this.placeholderSearch = 'Scratch a cat';
    this.searchText = '';
    /* labels */
    this.labelSearch = 'What to do';
    this.labelDate = 'When do it';
    this.labelCategory = 'Select category';
    this.labelSubcategory = 'Select subcategory';
    this.labelButton = 'Search';
    /* datepicker settings */
    this.mode = 'date';           /* select day */
    this.firstDayOfWeek = '1';    /* start calendar from first day of week */
    this.selCategory = 'All categories';
    this.selSubcategory = 'All subcategories';
    this.categories = ['Category1', 'Category2', 'Category3'];
    this.subcategories = ['Subcategory1', 'Subcategory2', 'Subcategory3'];
  }

  searchVendor() {
    if (this.searchDate === undefined) {
      this.searchDate = new Date();
    }
    if (this.searchText === '') {
      this.searchText = this.placeholderSearch;
    }
    console.log(this.searchText);
    console.log(this.searchDate);
  }

  setCategory(category: string) {
    this.selCategory = category;
  }

  setSubcategory(subcategory: string) {
    this.selSubcategory = subcategory;
  }

}
