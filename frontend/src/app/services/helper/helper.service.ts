import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

// Helpers
import { JwtHelper } from '../../helpers/jwthelper';
import { RoleRouter } from '../../helpers/rolerouter';

@Injectable()
export class HelperService {

  constructor(private router: Router) { }

  public redirectAfterAuthentication() {
    const userClaims = new JwtHelper().decodeToken(localStorage.getItem('token'));
    let path = new RoleRouter().getRouteByRole(userClaims['roleid']);
    this.router.navigate([path, userClaims['id']]);
  }

}
