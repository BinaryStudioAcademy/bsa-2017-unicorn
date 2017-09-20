import { Injectable } from '@angular/core';

import { JwtHelper } from '../../helpers/jwthelper';
import { RoleRouter } from '../../helpers/rolerouter';

@Injectable()
export class TokenHelperService {
  private jwtHeper: JwtHelper;

  constructor() {
    this.jwtHeper = new JwtHelper();
  }

  public isTokenValid(): boolean {
    const token = localStorage.getItem('token');
    try {
      this.jwtHeper.decodeToken(token);
      return true;
    }
    catch (Exception) {
      return false;
    }
  }

  public isTokenNotExpired(): boolean {
    const token = localStorage.getItem('token');
    return token != null && !this.jwtHeper.isTokenExpired(token);
  }

  public isAdminAccount(): boolean {
    const roleName = this.getRoleName();
    return this.isTokenValid() && this.isTokenNotExpired() && roleName === 'admin';
  }

  public isAccountBanned(): boolean {
    return this.getClaimByName('isbanned') && this.getClaimByName('isbanned').toLowerCase() === 'true';
  }

  public getAllClaims(): object {
    const token = localStorage.getItem('token');
    let claims;
    try {
      claims = this.jwtHeper.decodeToken(token);
    }
    catch (Exception) {
      claims = null;
    }

    return claims;
  }

  public getClaimByName(name: string): string {
    let claim = this.getAllClaims();
    return claim == null ? null : claim[name];
  }

  public getToken(): string {
    return localStorage.getItem('token');
  }

  public getRoleName(): string {
    let roleId = this.getClaimByName('roleid');
    let roleName = '';
    switch (roleId) {
      case '2':
          roleName = 'user';
          break;
      case '3':
          roleName = 'vendor';
          break;
      case '4':
          roleName = 'company';
          break;
      case '5':
          roleName = 'admin';
          break;
    }
    return roleName;
  }
}
