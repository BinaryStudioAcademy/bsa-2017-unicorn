import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DataService } from '../services/data.service';

import { Company } from '../register/models/company';
import { Customer } from '../register/models/customer';
import { Vendor } from '../register/models/vendor';

@Injectable()
export class RegisterService {

  constructor(
    private dataService: DataService) {
      this.dataService.setHeader('Content-Type', 'application/json');
    }

  checkAuthorized(provider: string, uid: string): Promise<any> {
    let url = `membership?provider=${provider}&uid=${uid}`;
    console.log(uid);
    return this.dataService.postFullRequest<string>('membership', {provider: provider, uid: uid});
  }

  confirmCustomer(customer: Customer): Promise<any> {
    return null;
  }

  confirmVendor(vendor: Vendor): Promise<any> {
    return null;
  }

  confirmCompany(company: Company): Promise<any> {
    return null;
  }

}
