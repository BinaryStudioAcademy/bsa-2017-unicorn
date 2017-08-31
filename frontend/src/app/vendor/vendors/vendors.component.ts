import { Component, OnInit } from '@angular/core';

import { NguiMapModule, Marker, NguiMap} from "@ngui/map";

import { Performer } from '../../models/performer.model';

import { PerformerService } from '../../services/performer.service';

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.sass']
})
export class VendorsComponent implements OnInit {

  loaded: boolean;

  /* pagination */
  pageSize = 10;
  maxSize = 3;
  hasEllipses = true;
  selectedPage = 1;
  tabSuffix = '?tab=reviews';
  performers: Performer[] = [];

  selected: string = '';

  constructor(
    private performerService: PerformerService
  ) { }

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.performerService.getAllPerformers().then(resp => {
      console.log(resp);
      this.performers = resp;
      this.loaded = true;
      
    });
  }

  onMapReady(map: NguiMap) {
    console.log(map);
  }

  select(name: string) {
    this.selected = name;
  }

  getPerformersPage(): Performer[] {
    return this.performers.slice((this.selectedPage - 1) * this.pageSize, this.selectedPage * this.pageSize);
  }

}
