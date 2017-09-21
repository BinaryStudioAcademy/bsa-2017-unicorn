import { NgModule } from '@angular/core';
import { SwitchComponent } from "./switch/switch.component"
import { RouterModule } from '@angular/router';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: 'switch',
        children: [
          {
            path: ':id',
            component: SwitchComponent,
          }
        ]
      }
    ])
  ],
  exports: [
    RouterModule
  ]
})
export class SwitchRoutingModule { }
