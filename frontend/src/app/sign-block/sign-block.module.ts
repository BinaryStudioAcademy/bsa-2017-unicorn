import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';
import { SignBlockComponent } from './sign-block/sign-block.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [SignBlockComponent],
  exports: [
    SignBlockComponent
  ]
})
export class SignBlockModule { }
