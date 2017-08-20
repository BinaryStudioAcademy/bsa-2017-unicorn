import { Component, OnInit, Input } from '@angular/core';

import { VendorBook } from '../../../models/book/vendor-book.model';
import { BookStatus } from '../../../models/book/book.model'

import { VendorService } from '../../../services/vendor.service';

@Component({
  selector: 'app-vendor-edit-orders',
  templateUrl: './vendor-edit-orders.component.html',
  styleUrls: ['./vendor-edit-orders.component.sass']
})
export class VendorEditOrdersComponent implements OnInit {
  @Input() vendorId: number;

  orders: VendorBook[];
  bookStatus = BookStatus;
  selectedStatus: BookStatus;
  constructor(private vendorService: VendorService) { }

  ngOnInit() {
    this.vendorService.getOrders(this.vendorId)
      .then(resp => this.orders = resp.body as VendorBook[]);
  }

}
