import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { User } from '../models/user'
import {Book} from '../models/book.model'
import {Vendor} from '../models/vendor'
import { environment } from "../../environments/environment";
import { Rating } from "../models/rating.model";
import { Review } from "../models/review.model";

@Injectable()
export class UserService {

  constructor(private dataService: DataService) {
     dataService.setHeader('Content-Type', 'application/json');
    }


  getUser(id: number):Promise<any>{
    return this.dataService.getFullRequest<User>("users/" + id)
    .then(res => { return res }); }


  updateUser(user: User): Promise<any> {

    return this.dataService.putFullRequest<User>('users/'+ user.Id, user)
      .catch(err => alert(err));
  }
  getRating(id: number): Promise<any> {
    return this.dataService.getFullRequest<Rating>('users/'+id+'/rating')
      .catch(err => alert(err));
  }
  getReviews(id: number): Promise<any> {
    return this.dataService.getFullRequest<Review[]>('users/'+id+'/reviews')
      .catch(err => alert(err));
  }
  
}
