import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { TokenHelperService } from '../../services/helper/tokenhelper.service'

@Component({
  selector: 'app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  title: string;
  slogan: string;
  labelSearch: string;
  placeholderCategory: string;
  placeholderSubcategory: string;
  searchCategory: string;
  searchSubcategory: string;
  labelDate: string;
  searchDate: Date;
  mode: string;
  firstDayOfWeek: string;
  categories: {}[];
  subcategories: string[];

  constructor(
    private router: Router,
    private tokenHelper: TokenHelperService
  ) {
    navigator.geolocation.getCurrentPosition(()=>{});
   }

  ngOnInit() {
    this.initContent();
  }

  initContent() {
    this.title = 'Happy unicorn';
    this.slogan = 'We scratch your cat right now, because we can.';
    this.placeholderCategory = 'SCRATCH';
    this.placeholderSubcategory = 'CAT';
    /* labels */
    this.labelSearch = 'What to do';
    this.labelDate = 'When to do it';
    /* datepicker settings */
    this.mode = 'date';           /* select day */
    this.firstDayOfWeek = '1';    /* start calendar from first day of week */

    this.categories = [{id: 1, text: 'Category1'}, {id: 2, text: 'Category2'}, {id: 3, text: 'Category3'}];
    this.subcategories = ['Subcategory1', 'Subcategory2', 'Subcategory3'];
  }

  searchPerformer() {
    let d: number;
    if (this.searchDate !== undefined) {
      d = this.searchDate.getTime() / 1000;
    }
    if (this.searchCategory === undefined && this.searchSubcategory === undefined && this.searchDate === undefined) {
      this.router.navigate(['/search']);
    } else {
      console.log(this.searchCategory, this.searchSubcategory, this.searchDate);
      this.router.navigate(['/search'], {
        queryParams: {
          'category': this.searchCategory,
          'subcategory': this.searchSubcategory,
          'date': d
        }
      });
    }
  }

  isPerformer(): boolean {
    let role = this.tokenHelper.getRoleName();
    return role === 'vendor' || role === 'company';
  }

}
