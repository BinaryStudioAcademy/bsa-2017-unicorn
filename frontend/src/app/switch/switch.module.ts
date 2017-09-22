import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SwitchRoutingModule } from "./switch-routing.module";
import { SwitchComponent } from "./switch/switch.component";
import { SuiDimmerModule } from "ng2-semantic-ui";

@NgModule({
  imports: [
    SwitchRoutingModule,
    SuiDimmerModule
  ],
  declarations: [
    SwitchComponent
  ]
})
export class SwitchModule { }
