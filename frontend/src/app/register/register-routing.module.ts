import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { RegisterComponent } from './register-component/register.component';

@NgModule({
  imports: [
    RouterModule.forChild([
      { path: "register", component: RegisterComponent }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class RegisterRoutingModule { }
