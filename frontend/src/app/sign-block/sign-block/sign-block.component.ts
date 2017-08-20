import { Component, OnInit } from '@angular/core';
import { NgModel } from '@angular/forms';

import { RegisterModal } from '../../register/register-component/register.component';
import { SuiModalService } from 'ng2-semantic-ui';

@Component({
  selector: 'app-sign-block',
  templateUrl: './sign-block.component.html',
  styleUrls: ['./sign-block.component.sass']
})
export class SignBlockComponent implements OnInit {

  constructor(private modalService: SuiModalService) { }
  ngOnInit() {
  }
  sign() {
    this.modalService.open(new RegisterModal());
  }
}
