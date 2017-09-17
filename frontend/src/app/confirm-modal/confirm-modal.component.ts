import { Component, OnInit } from '@angular/core';
import { SuiModal, ComponentModalConfig } from 'ng2-semantic-ui';

interface IConfirmModalContext {
  title: string;
  question: string;
  action: string;
}

export class ConfirmModal extends ComponentModalConfig<IConfirmModalContext, void, void> {
  constructor(title: string, question: string, action: string) {
    super(ConfirmModalComponent, { title, question, action });

    this.isClosable = true;
    this.isInverted = true;
    this.size = "small";
  }
}

@Component({
  selector: 'app-confirm-modal',
  templateUrl: './confirm-modal.component.html',
  styleUrls: ['./confirm-modal.component.sass']
})
export class ConfirmModalComponent implements OnInit {

  constructor(public modal: SuiModal<IConfirmModalContext, void, void>,) { }

  ngOnInit() {
  }

}
