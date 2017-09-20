import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { TokenHelperService } from '../services/helper/tokenhelper.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router, private tokenHelper: TokenHelperService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired() && !this.tokenHelper.isAccountBanned()) {
      return true;
    } else {
      localStorage.removeItem('token');
    }

    // not logged in so redirect to landing page
    this.router.navigate(['/index']);
    return false;
  }
}
