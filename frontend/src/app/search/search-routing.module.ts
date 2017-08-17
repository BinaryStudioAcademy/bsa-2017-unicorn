import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { SearchComponent } from './search-component/search.component';

@NgModule({
  imports: [
    RouterModule.forRoot([
      {
        path: 'search/:category/:subcategory/:date',
        component: SearchComponent,
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class SearchRoutingModule { }
