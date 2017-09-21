import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SwitchRoutingModule } from "./switch-routing.module";
import { SwitchComponent } from "./switch/switch.component";

@NgModule({
  imports: [
    SwitchRoutingModule
  ],
  declarations: [
    SwitchComponent
  ]
})
export class SwitchModule { }
