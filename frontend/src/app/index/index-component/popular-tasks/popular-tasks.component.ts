import { Component, OnInit } from '@angular/core';

import { PopularCategory } from '../../../models/popular/PopularCategory';
import { Performer } from '../../../models/popular/Performer';

import { PopularService } from '../../../services/popular.service';

@Component({
  selector: 'popular-tasks',
  templateUrl: './popular-tasks.component.html',
  styleUrls: ['./popular-tasks.component.css']
})
export class PopularTasksComponent implements OnInit {
  popularVendors: MockVendor[];
  cleaningVendors: MockVendor[];
  buildingVendors: MockVendor[];
  geriatricVendors: MockVendor[];

  categories: PopularCategory[];
  bestPerformers: Performer[];

  fperformers: Performer[];
  sperformers: Performer[];
  tperformers: Performer[];

  reviewsTab = 'reviews';

  loaded: boolean = false;

  constructor(private popularService: PopularService) { }

  ngOnInit() {
    this.createMockVendors();

    this.popularService.loadCategories().then(c => {
      this.categories = c;
      console.log(this.categories);
      console.log(this.categories[0].Name);
      this.initPerformers();
    });

    this.popularService.loadPopularPerformers().then(p => {
      this.bestPerformers = p;
      console.log(this.bestPerformers);
    });

    
  }

  initPerformers() {
    this.popularService.loadPerformers(this.categories[0].Id).then(p => {
      this.fperformers = p;
    });
    this.popularService.loadPerformers(this.categories[1].Id).then(p => {
      this.sperformers = p;
    });
    this.popularService.loadPerformers(this.categories[2].Id).then(p => {
      this.tperformers = p;
    });
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
      imgUrl: 'http://unmaidsservices.polakpiotr.com/wp-content/uploads/2015/04/Fort-Lauderdale-Cleaning-Services-1.jpg'
    }, {
      id: 4,
      name: 'Company 4',
      imgUrl: 'http://cleaning-kiev.com.ua/images/stati/new/uborka-kvartir-posle-remonta-kiev-tseny.jpg'
    }, {
      id: 5,
      name: 'Company 5',
      imgUrl: 'http://fanhome.ru/wp-content/uploads/2012/03/uborka.jpg'
    }];

    this.buildingVendors = [{
      id: 1,
      name: 'Company 1',
      imgUrl: 'http://www.comparequotes.net.au/Images/building/builder.jpg'
    }, {
      id: 2,
      name: 'Company 2',
      imgUrl: 'http://i.telegraph.co.uk/multimedia/archive/02676/Philippa_Amy_Tutti_2676616b.jpg'
    }, {
      id: 3,
      name: 'Company 3',
      imgUrl: 'https://wallpaperscraft.ru/image/stroitel_stroyka_kaska_planshet_79895_3840x2400.jpg'
    }, {
      id: 4,
      name: 'Company 4',
      imgUrl: 'http://i.telegraph.co.uk/multimedia/archive/02366/build_2366824b.jpg'
    }, {
      id: 5,
      name: 'Company 5',
      imgUrl: 'http://simsar.az/up/news/article/2015/04/10/150410_4966.jpg'
    }];

    this.geriatricVendors = [{
      id: 1,
      name: 'Company 1',
      imgUrl: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTxUNWmsckgWas2YwDCmOHEZ4B5JR62bcobGiaLeE3TLJrI-k1c'
    }, {
      id: 2,
      name: 'Company 2',
      imgUrl: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTHFYvHdBO6iSRj5Lu87wZfSUwmXoMV6YDhuPDVRagJxjNMQ_2Y'
    }, {
      id: 3,
      name: 'Company 3',
      imgUrl: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR5BU45A_bjQ7wmHIMPnIetDgsjbCN_Z3gqbqbRKYCnS7d9NBUKtw'
    }, {
      id: 4,
      name: 'Company 4',
      imgUrl: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTAfWipS0ELuH3hLItRRE46bTw01qT7LNZG8pKY7S-urESf5Q1GKQ'
    }, {
      id: 5,
      name: 'Company 5',
      imgUrl: 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSla4VyL8i4Jd5KRq7LdBj7kNWxxid-S64F9QE6X_SX4nsYPg4k'
    }];
  }

}

export class MockVendor {
  id: number;
  name: string;
  imgUrl: string;
}
