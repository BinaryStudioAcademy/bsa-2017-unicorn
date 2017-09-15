import { Component, OnInit } from '@angular/core';
import { AuthModal } from "./auth-modal/auth-modal.component";
import { SuiModalService } from "ng2-semantic-ui/dist";

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.sass']
})
export class AdminComponent implements OnInit {

  constructor(private modalService: SuiModalService) { }

  ngOnInit() {
    this.modalService
      .open(new AuthModal("tiny"));
  }

}
