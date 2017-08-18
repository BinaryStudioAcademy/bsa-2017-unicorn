import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { User } from '../models/user'
import {Book} from '../models/book.model'
import {Vendor} from '../models/vendor'
import { environment } from "../../environments/environment";

@Injectable()
export class UserService {

  constructor(private dataService: DataService) {
     dataService.setHeader('Content-Type', 'application/json');
    }

  getUser(id: number):Promise<User>{
    return this.dataService.getRequest<User>(environment.apiUrl + "user/" + id)
    .then(res => { return res }); }


  updateUser(user: User): Promise<any> {
    return this.dataService.putFullRequest<User>(`${"users"}/${user.id}`, user)
      .catch(err => alert(err));
  }
  saveUser(user: User){
    this.dataService.postRequest("user", user);
  }
  
}
