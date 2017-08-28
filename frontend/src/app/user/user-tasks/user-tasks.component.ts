import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { User } from '../../models/user';
import { NgModel } from '@angular/forms';

import {SuiModalService, TemplateModalConfig, ModalTemplate, ModalSize} from 'ng2-semantic-ui';

import { CustomerbookService } from '../../services/customerbook.service';

import { CustomerBook, BookStatus } from '../../models/book/book.model';

@Component({
  selector: 'app-user-tasks',
  templateUrl: './user-tasks.component.html',
  styleUrls: ['./user-tasks.component.sass']
})
export class UserTasksComponent implements OnInit {
  
  @ViewChild('modalTemplate')
  public modalTemplate:ModalTemplate<void, string, string>

  @Input() user:User;
  rev: string;

  books: CustomerBook[];

  constructor(
    private bookService: CustomerbookService,
    private modalService: SuiModalService
  ) { }

  ngOnInit() {
    this.bookService.getCustomerBooks(this.user.Id)
      .then(resp => {
        this.books = resp;
      });
  }

  getStatus(id: number): string {
    let status = this.books.filter(b => b.Id == id)[0].Status;
    switch (status) {
      case BookStatus.Pending: return 'Pending';
      case BookStatus.Accepted: return 'Accepted';
      case BookStatus.Finished: return 'Finished';
      case BookStatus.Confirmed: return 'Rated';
      default: return 'error';
    }
  }

  isFinished(id: number): boolean {
    return this.books.filter(b => b.Id == id)[0].Status == BookStatus.Finished;
  }

  openModal() {
    const config = new TemplateModalConfig<void, string, string>(this.modalTemplate);
    config.isInverted = true;
    config.size = ModalSize.Tiny;
    this.modalService.open(config);
  }

}
