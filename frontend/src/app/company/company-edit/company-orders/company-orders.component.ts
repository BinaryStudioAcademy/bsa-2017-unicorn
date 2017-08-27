import { Component, OnInit } from '@angular/core';
import { CompanyBooks } from "../../../models/company-page/company-books.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { BookStatus } from "../../../models/book/book.model";
import { CompanyBook } from "../../../models/company-page/company-book.model";


@Component({
  selector: 'app-company-orders',
  templateUrl: './company-orders.component.html',
  styleUrls: ['./company-orders.component.sass']
})
export class CompanyOrdersComponent implements OnInit {
  company: CompanyBooks;
  bookStatus = BookStatus;
  isLoaded: boolean = false;
  changedOrders: CompanyBooks = { Id: null, Books: [] };

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute, ) { }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.companyService.getCompanyBooks(params['id'])).subscribe(res => {
        this.company = res;
      });
  }

  isOrderChanged(order): boolean {
    return this.changedOrders.Books.find(o => o.Id === order.Id) !== undefined
  }

  onOrderChange(order: CompanyBook): void {
    if (this.changedOrders.Books.find(o => o.Id === order.Id) === undefined) {
      this.changedOrders.Books.push(order);
    }
  }

  saveOrder(order: CompanyBook): void {
    this.isLoaded = true;
    //this.company.Books.splice(this.company.Books.findIndex(o => o.Id === order.Id), 1, order);
    this.companyService.saveCompanyBook(order).then(() => {
      this.changedOrders.Books.splice(this.changedOrders.Books.findIndex(o => o.Id === order.Id), 1);
      this.isLoaded = false;
    });
  }

}
