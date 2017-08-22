import { Component, OnInit } from '@angular/core';
import { CompanyBooks } from "../../../models/company-page/company-books.model";
import { CompanyService } from "../../../services/company-services/company.service";
import { ActivatedRoute, Params } from "@angular/router";
import { BookStatus } from "../../../models/book/book.model";


@Component({
  selector: 'app-company-orders',
  templateUrl: './company-orders.component.html',
  styleUrls: ['./company-orders.component.sass']
})
export class CompanyOrdersComponent implements OnInit {
  company: CompanyBooks;
  bookStatus = BookStatus;

  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyBooks(params['id'])).subscribe(res => {
      this.company = res;
    });
  }

}
