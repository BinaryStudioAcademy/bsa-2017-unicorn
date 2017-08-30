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

  changedOrders: VendorBook[];

  constructor(private vendorService: VendorService) { }

  isOrderChanged(order): boolean {
    return this.changedOrders.find(o => o.Id === order.Id) !== undefined
  }

  onOrderChange(order: VendorBook): void {
    if (this.changedOrders.find(o => o.Id === order.Id) === undefined) {
      this.changedOrders.push(order);
    }
  }

  saveOrder(order: VendorBook): void {
    this.vendorService.updateOrder(this.vendorId, order);
    this.changedOrders.splice(this.changedOrders.findIndex(o => o.Id === order.Id), 1);
  }

  refreshOrders(): void {
    this.vendorService.getOrders(this.vendorId)
    .then(resp => this.orders = resp.body as VendorBook[]);
  }

  ngOnInit() {
    this.changedOrders = [];
    this.refreshOrders();
  }

}
