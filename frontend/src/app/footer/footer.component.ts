import { Component, OnInit, ViewChild } from '@angular/core';

import { SuiModule } from 'ng2-semantic-ui';
import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import {ToastsManager, Toast} from 'ng2-toastr';

import { ReportService } from '../services/report.service';
import { AccountService } from '../services/account.service';
import { TokenHelperService } from '../services/helper/tokenhelper.service';


import { Report } from '../models/report/report.model';
import { ReportType } from '../models/report/reportType.model';
import { ProfileShortInfo } from '../models/profile-short-info.model';
import { FeedbackModal } from '../feedback-modal/feedback-modal.component';


@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.sass'],
  providers: [ReportService]
})
export class FooterComponent implements OnInit {

  constructor(
    private modalService: SuiModalService
    ) {  }

  ngOnInit() { }

  openFeedbackModal(): void {
    this.modalService.open(new FeedbackModal("Leave Feedback Here", ReportType.feedback));
  }
}
