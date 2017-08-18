import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { User } from '../models/user'
import {Book} from '../models/book.model'
import {Vendor} from '../models/vendor'
import { environment } from "../../environments/environment";

@Injectable()
export class UserService {

  constructor(private dataService: DataService) { }

  getUser(id: number):Promise<User>{
    return this.dataService.getRequest<User>(environment.apiUrl + "user/" + id)
    .then(res => { return res }); }

  updateUser(id:number, user:User)
  {
    this.dataService.postRequest("",user);
  }

}
