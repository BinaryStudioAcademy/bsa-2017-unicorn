import { Component, OnInit, ViewChild } from '@angular/core';

import { SuiModule } from 'ng2-semantic-ui';
import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import {ToastsManager, Toast} from 'ng2-toastr';

import { ModalService } from '../services/modal/modal.service';



@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.sass'],
  providers: [ModalService]
})
export class FooterComponent implements OnInit {
  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<void, {}, void>;
  private activeModal: SuiActiveModal<void, {}, void>;

  writtenMessage: string;

  constructor(
    private modalService: ModalService,
    private toastr: ToastsManager
    ) { }

  ngOnInit() {
  }

  openModal() {
    this.writtenMessage = '';
    this.activeModal = this.modalService.openModal(this.modalTemplate, ModalSize.Mini);
  }

  sendMessage() {
    this.toastr.success('Your message was sent');
    this.activeModal.approve('approved');
  }
}
