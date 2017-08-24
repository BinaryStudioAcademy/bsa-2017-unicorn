import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { RoleRouter } from '../../helpers/rolerouter';
import { TokenHelperService } from './tokenhelper.service';

@Injectable()
export class HelperService {

  constructor(private router: Router, private tokenHelper: TokenHelperService) { }

  public redirectAfterAuthentication() {
    const userClaims = this.tokenHelper.getAllClaims();
    const userRoleId = +userClaims['roleid'];

    let path = new RoleRouter().getRouteByRole(userRoleId);
    userRoleId === 2 ? this.router.navigate([path]) : this.router.navigate([path, userClaims['profileid']]);
  }
}
