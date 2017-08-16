import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { JwtHelper } from '../helpers/jwthelper';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (localStorage.getItem('token')) {

      if (this.tokenNotExpired()) {
        return true;
      } else {
        localStorage.removeItem('token');
      }
    }

    // not logged in so redirect to landing page
    this.router.navigate(['/index']);
    return false;
  }

  private tokenNotExpired() {
    const token = localStorage.getItem('token');
    const jwtHelper = new JwtHelper();
    return token != null && !jwtHelper.isTokenExpired(token);
  }
}
