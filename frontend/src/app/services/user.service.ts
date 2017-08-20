import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { User } from '../models/user'
import {Book} from '../models/book/book.model'
import {Vendor} from '../models/vendor'
import { environment } from "../../environments/environment";

@Injectable()
export class UserService {

  constructor(private dataService: DataService) {
     dataService.setHeader('Content-Type', 'application/json');
  }


  getUser(id: number):Promise<any>{
    return this.dataService.getFullRequest<User>("users/" + id)
    .then(res => { return res });
  }


  updateUser(user: User): Promise<any> {

    return this.dataService.putFullRequest<User>('users/'+ user.Id, user)
      .catch(err => alert(err));
  }
  
}
