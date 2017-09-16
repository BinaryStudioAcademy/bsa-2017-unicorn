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

  confirmCustomer(customer: Customer): Promise<any> {
    
    return this.dataService.postFullRequest<any>('membership/customer', customer)
      .catch(err => console.log(err));
  }

  confirmVendor(vendor: Vendor): Promise<any> {
    return this.dataService.postFullRequest<any>('membership/vendor', vendor)
      .catch(err => console.log(err));
  }

  confirmCompany(company: Company): Promise<any> {
    return this.dataService.postFullRequest<any>('membership/company', company)
      .catch(err => console.log(err));
  }

}
