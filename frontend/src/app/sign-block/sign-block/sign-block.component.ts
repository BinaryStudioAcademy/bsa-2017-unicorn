import { Component, OnInit} from '@angular/core';
import { NgModel } from '@angular/forms';
import {SuiModule} from 'ng2-semantic-ui';
import * as firebase from 'firebase/app';
import { AuthService } from '../../services/auth/auth.service';
import { ConfirmModal, IConfirmModalContext } from '../../register/register-component/register.component';
import { SuiModalService, TemplateModalConfig, SuiModal, ComponentModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

@Component({
  selector: 'app-sign-block',
  templateUrl: './sign-block.component.html',
  styleUrls: ['./sign-block.component.sass']
})
export class SignBlockComponent implements OnInit {
  
  constructor(private modalService: SuiModalService) { }
  ngOnInit() {
  }
  sign()
  {
    this.modalService
    .open(new ConfirmModal("Are you sure?", "Are you sure about accepting this?"))
    .onApprove(() => alert("User has accepted."))
    .onDeny(() => (''));
  }
}
