import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'popular-tasks',
  templateUrl: './popular-tasks.component.html',
  styleUrls: ['./popular-tasks.component.css']
})
export class PopularTasksComponent implements OnInit {
  popularVendors: MockVendor[];
  cleaningVendors: MockVendor[];

  constructor() { }

  ngOnInit() {
    this.createMockVendors();
  }

  createMockVendors() {
    this.popularVendors = [{
      id: 1,
      name: 'Company 1',
      imgUrl: 'http://animal.ru/i/upload/137718097676.jpg'
    }, {
      id: 2,
      name: 'Company 2',
      imgUrl: 'http://animal.ru/i/upload/137718083819.jpg'
    }, {
      id: 3,
      name: 'Company 3',
      imgUrl: 'http://s1.img.yan.vn//YanNews/2167221/201510/20151006-111922-8_520x346.jpg'
    }, {
      id: 4,
      name: 'Company 4',
      imgUrl: 'http://www.tinydog.ru/wp-content/uploads/2016/09/chistka-ushej-koshki-v-domashnix-usloviyax-8.jpg'
    }, {
      id: 5,
      name: 'Company 5',
      imgUrl: 'http://animal.ru/i/upload/137718089190.jpg'
    }];

    this.cleaningVendors = [{
      id: 1,
      name: 'Company 1',
      imgUrl: 'http://storinka.com.ua/wp-content/uploads/2015/12/uborka.jpg'
    }, {
      id: 2,
      name: 'Company 2',
      imgUrl: 'http://zulfiya.com.ua/img/cms/July/uborka_3.jpg'
    }, {
      id: 3,
      name: 'Company 3',
      imgUrl: 'http://toantamgroup.net/kcfinder/upload/images/z13.jpg'
    }, {
      id: 4,
      name: 'Company 4',
      imgUrl: 'http://cleaning-kiev.com.ua/images/stati/new/uborka-kvartir-posle-remonta-kiev-tseny.jpg'
    }, {
      id: 5,
      name: 'Company 5',
      imgUrl: 'http://fanhome.ru/wp-content/uploads/2012/03/uborka.jpg'
    }];
  }

}

export class MockVendor {
  id: number;
  name: string;
  imgUrl: string;
}
