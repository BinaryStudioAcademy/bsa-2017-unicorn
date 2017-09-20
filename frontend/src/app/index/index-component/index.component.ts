import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { CategoryService } from '../../services/category.service';

import { SearchTag } from '../../models/search/search-tag';
import { Category } from '../../models/category.model';
import { Subcategory } from '../../models/subcategory.model';

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
  // categories: {}[];
  categories: Category[];
  subcategories: string[];
  filterCtgs: SearchTag[] = [];
  filterSubctgs: SearchTag[] = [];

  constructor(
    private router: Router,
    private tokenHelper: TokenHelperService,
    private categoryService: CategoryService
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

    this.categoryService.getAll()
    .then(resp => {
      this.categories = resp.body as Category[];
    });
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
    return (role === 'vendor' || role === 'company') && !this.tokenHelper.isAccountBanned();
  }

  filter(arr, search = '') {
    const result = [];
    if (arr) {
      if (search !== '') {
        for (let i = 0; i < arr.length; i++) {
          const tags = arr[i].Tags.split(',');
          for (let j = 0; j < tags.length; j++) {
            const tag = tags[j].toLowerCase();
            let input = search.toLowerCase();
            if (tag.indexOf(input) > -1) {
              let start = tag.substring(0, tag.indexOf(input));
              const end = tag.substring(tag.indexOf(input) + input.length);
              if (start.length > 0) {
                start = this.capitalizeFirstLetter(start);
              } else {
                input = this.capitalizeFirstLetter(input);
              }
              const html = start + '<b>' + input + '</b>' + end;
              const tagObj = {
                Name: tags[j],
                Html: html,
                Value: arr[i].Name,
                Group: arr[i].Name,
                Icon: arr[i].Icon
              };
              result.push(tagObj);
              if (result.length > 30) {
                return result;
              }
            }
          }
        }
      } else {
        for (let i = 0; i < arr.length; i++) {
          const tagObj = {
            Name: arr[i].Name,
            Html: arr[i].Name,
            Value: arr[i].Name,
            Group: '',
            Icon: arr[i].Icon
          };
          result.push(tagObj);
          if (result.length > 30) {
            return result;
          }
        }
      }
    }
    return result;
  }


  filterCategory() {
    this.filterCtgs = this.filter(this.categories, this.searchCategory);
  }

  filterSubcategory() {
    let subcategories = [];
    if (this.searchCategory) {
      const category = this.categories.find(c => c.Name === this.searchCategory);
      if (category) {
        subcategories = category.Subcategories;
      } else {
        return;
      }
    } else {
      subcategories = getAllSubcategories(this.categories);
    }
    if (subcategories) {
      this.filterSubctgs = this.filter(subcategories, this.searchSubcategory);
    }

    function getAllSubcategories(categories) {
      let result = [];
      if (categories) {
        for (let i = 0; i < categories.length; i++) {
          result = result.concat(categories[i].Subcategories);
        }
      }
      return result;
    }
  }

  selectCategory(item) {
    this.searchCategory = this.capitalizeFirstLetter(item.Value);
    this.filterCtgs = [];
    this.searchSubcategory = undefined;
  }

  selectSubcategory(item) {
    this.searchSubcategory = this.capitalizeFirstLetter(item.Value);
    this.filterSubctgs = [];
  }

  capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
  }

  onClickedOutside(e: Event) {
    switch (e.srcElement.id) {
      case 'ctg':
        this.filterSubctgs = [];
        break;
      case 'subctg':
        this.filterCtgs = [];
        break;
      default:
        this.filterCtgs = [];
        this.filterSubctgs = [];
        break;
    }
  }

}
