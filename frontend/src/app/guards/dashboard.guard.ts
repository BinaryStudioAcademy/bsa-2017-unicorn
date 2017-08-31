import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { TokenHelperService } from '../services/helper/tokenhelper.service';

@Injectable()
export class DashboardGuard implements CanActivate {

  constructor(
    private router: Router,
    private tokenHelper: TokenHelperService) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    let role = this.tokenHelper.getRoleName();
    let hasPermission = role === 'vendor' || role === 'company' || role === 'admin';
    if (hasPermission) {
      return true;
    }
    this.router.navigate(['/index']);
    return false;
  }
}
