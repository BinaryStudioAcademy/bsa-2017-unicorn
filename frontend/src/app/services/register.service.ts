import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { DataService } from '../services/data.service';

@Injectable()
export class RegisterService {

  constructor(
    private dataService: DataService,
    private http: HttpClient) { }

  checkAuthorized(provider: string, uid: string): Promise<any> {
    let url = `membership?provider=${provider}&uid=123`;
    return this.dataService.getFullRequest<any>(url);
    //console.log(url);
    //return this.dataService.getRequest<any>(url);
  }

}
