import { Injectable } from '@angular/core';

import { DataService } from '../services/data.service';

@Injectable()
export class RegisterService {

  constructor(private dataService: DataService) { }

  checkAuthorized(provider: string, uid: number): void {
    let url = `membership?provider=${provider}&uid=${uid}`;
    console.log(url);
    this.dataService.getRequest<string>(url)
      .then(c => {
        console.log(c);
        return true;
      })
      .catch(e => {
        console.log(e);
        return false;
      });
  }

}
