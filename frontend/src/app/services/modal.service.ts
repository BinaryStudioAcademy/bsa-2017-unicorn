import { Injectable } from '@angular/core';

import { SuiModalService, TemplateModalConfig, SuiModal, ComponentModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import { ConfirmModal, IConfirmModalContext } from '../register/register-component/register.component';

@Injectable()
export class ModalService {

  public activeModal: SuiActiveModal<IConfirmModalContext, {}, void>;
  constructor(private modalService: SuiModalService) { }

  public open() {
    this.activeModal = this.modalService
    .open(new ConfirmModal("Are you sure?", "Are you sure about accepting this?"))
    .onApprove(() => alert("User has accepted."))
    .onDeny(() => {});
  }

  public close() {
    this.activeModal.deny(null);
  }

}
