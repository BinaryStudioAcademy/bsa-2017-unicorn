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
    console.log('roelid', userRoleId);

    if (userRoleId === 3 || userRoleId === 4) {
      this.router.navigate(['dashboard']);
      return;
    }

    let path = new RoleRouter().getRouteByRole(userRoleId);
    userRoleId === 2 ? this.router.navigate([path]) : this.router.navigate([path, userClaims['profileid']]);
  }
}
