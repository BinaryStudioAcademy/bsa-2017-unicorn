import { Component, OnInit } from '@angular/core';

import {SuiModal, ComponentModalConfig, ModalSize} from "ng2-semantic-ui";

import { Review } from '../../models/review.model';

export interface IReviewModalContext {
  review: Review,
}

export class ReviewModal extends ComponentModalConfig<IReviewModalContext, void, void> {
  constructor(review: Review) {
    super(ReviewModalComponent, { review });
    this.isInverted = true;
    this.size = ModalSize.Tiny;
  }
}

@Component({
  selector: 'app-review-modal',
  templateUrl: './review-modal.component.html',
  styleUrls: ['./review-modal.component.sass']
})
export class ReviewModalComponent implements OnInit {

  constructor(
    public modal: SuiModal<IReviewModalContext, void, void>
  ) { }

  ngOnInit() {
  }

}
