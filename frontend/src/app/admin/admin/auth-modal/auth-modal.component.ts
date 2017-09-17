import { Component, OnInit } from '@angular/core';

import {SuiModal, ComponentModalConfig, ModalSize} from "ng2-semantic-ui"
import { AdminAuthService } from "../../../services/admin-auth.service";

import { AuthenticationLoginService } from '../../../services/auth/authenticationlogin.service';
import { AuthenticationEventService } from '../../../services/events/authenticationevent.service';

interface IAuthModalContext {
}

export class AuthModal extends ComponentModalConfig<IAuthModalContext, void, void> {
  constructor(size = ModalSize.Small) {
      super(AuthModalComponent, { });

      this.isClosable = false;
      this.isInverted = true;
      this.size = size;
  }
}

@Component({
    selector: 'admin-auth-modal',
    templateUrl: './auth-modal.component.html',
    styleUrls: ['./auth-modal.component.sass']
})

export class AuthModalComponent {
  login: string;
  password: string;

  isValid: boolean = true;

  constructor(
    public modal:SuiModal<IAuthModalContext, void, void>,
    private authLoginService: AuthenticationLoginService,
    private authEventService: AuthenticationEventService,
    private adminAuthService: AdminAuthService
  ) {}

    signIn(): void {
      this.adminAuthService.signIn(this.login, this.password)
        .then((response) => {
          this.authLoginService.saveJwt(response.headers.get('token'));
          this.authEventService.signIn();
          this.modal.approve(undefined);
        })
        .catch(() => this.isValid = false);
    }
}
