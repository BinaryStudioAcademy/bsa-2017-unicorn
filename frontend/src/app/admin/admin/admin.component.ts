import { Component, OnInit } from '@angular/core';
import { AuthModal } from "./auth-modal/auth-modal.component";
import { SuiModalService } from "ng2-semantic-ui/dist";
import { TokenHelperService } from "../../services/helper/tokenhelper.service";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass']
})
export class AdminComponent implements OnInit {

  constructor(private modalService: SuiModalService, private tokenHelper: TokenHelperService) { }

  ngOnInit() {
    if (!this.tokenHelper.isAdminAccount()) {
      this.modalService
        .open(new AuthModal("tiny"));
    }
  }

}
