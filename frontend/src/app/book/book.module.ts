import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';

import { BookComponent } from './book/book.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SuiModule
  ],
  declarations: [BookComponent],
  exports: [
    BookComponent
  ]
})
export class BookModule { }
