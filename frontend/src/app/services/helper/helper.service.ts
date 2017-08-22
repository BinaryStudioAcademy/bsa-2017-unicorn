import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { RoleRouter } from '../../helpers/rolerouter';
import { TokenHelperService } from './tokenhelper.service';

@Injectable()
export class HelperService {

  constructor(private router: Router, private tokenHelper: TokenHelperService) { }

  public redirectAfterAuthentication() {
    const userClaims = this.tokenHelper.getAllClaims();
    let path = new RoleRouter().getRouteByRole(userClaims['roleid']);
    this.router.navigate([path, userClaims['profileid']]);
  }
}
