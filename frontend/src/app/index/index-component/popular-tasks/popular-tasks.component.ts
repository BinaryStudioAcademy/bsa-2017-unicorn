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
      imgUrl: 'http://hostaculous.net/wp-content/uploads/2016/10/shutterstock_246224482.jpg'
    }, {
      id: 2,
      name: 'Company 2',
      imgUrl: 'http://cdn.newsday.com/polopoly_fs/1.5805337.1481242059!/httpImage/image.jpg_gen/derivatives/display_960/image.jpg'
    }, {
      id: 3,
      name: 'Company 3',
      imgUrl: 'http://clv.h-cdn.co/assets/17/11/640x960/gallery-1489551165-home-town-ben-and-erin-napier-2.jpg'
    }, {
      id: 4,
      name: 'Company 4',
      imgUrl: 'https://www.adn.com/resizer/hMqnHZI4RUAebymsbnCLn6OTnAE=/1200x0/s3.amazonaws.com/arc-wordpress-client-uploads/adn/wp-content/uploads/2016/06/21185739/marty-raney-1024x647.jpg'
    }, {
      id: 5,
      name: 'Company 5',
      imgUrl: 'http://i.huffpost.com/gen/872521/thumbs/o-MILLION-DOLLAR-DECORATORS-facebook.jpg'
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
