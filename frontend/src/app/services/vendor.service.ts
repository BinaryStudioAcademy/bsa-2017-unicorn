import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Vendor } from '../models/vendor.model'

@Injectable()
export class VendorService {

  constructor(private dataService: DataService) { }

  getVendorById(id: number) : Vendor {
    return;
  }

}
