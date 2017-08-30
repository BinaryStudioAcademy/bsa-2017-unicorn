import { Component, OnInit } from '@angular/core';

import { Performer } from '../../models/performer.model';

import { PerformerService } from '../../services/performer.service';

@Component({
  selector: 'app-vendors',
  templateUrl: './vendors.component.html',
  styleUrls: ['./vendors.component.sass']
})
export class VendorsComponent implements OnInit {

  performers: Performer[] = [];

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
    });
  }

}
