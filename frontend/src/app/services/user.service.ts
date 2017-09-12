import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { User } from '../models/user'
import { Book } from '../models/book/book.model'
import { Vendor } from '../models/vendor.model'
import { environment } from "../../environments/environment";
import { Review } from "../models/review.model";
import { Router } from '@angular/router';
@Injectable()
export class UserService {

  constructor(private dataService: DataService,  private router: Router) {
    dataService.setHeader('Content-Type', 'application/json');
  }

  getUser(id: number): Promise<any> {
    return this.dataService.getFullRequest<User>("users/" + id)
      .then(res => { return res }) 
      .catch(err => this.router.navigate([`not-found`], {
        queryParams: {
          message: `this user doesn’t exist. Try to find someone else.`,
        }}));
    
  }

  getUserForOrder(id: number): Promise<any> {
    return this.dataService.getRequest<User>("users/" + id + "/order")
      .then(res => { return res })
      .catch(err => location.href = 'index');
  }

  // getUserOld(id: number): User {
  //     var user: User = {
  //         id: 0,
  //         isDeleted: false,
  //         firstName: "Vasya",
  //         middleName: "",
  //         surName: "Pupkin",
  //         location: "Dnipro",
  //         books: [{
  //             date: new Date(Date.now()),
  //             description: "fix dripping tap",
  //             status: "In Progress",
  //             workType: "House work",
  //             address: "",
  //             contact: "",
  //             vendor: {
  //                 FIO: "Petr Petrov",
  //                 Avatar: "",
  //                 Experience: 0,
  //                 Position: "",
  //             }
  //         }],
  //         gender: "",
  //         email: "",
  //         phone: "",
  //         dateCreated: null,
  //         avatarUrl: "http://scontent-sea1-1.cdninstagram.com/t51.2885-15/s480x480/e35/10349706_559451967536913_1168844394_n.jpg?ig_cache_key=MTA1NTAyNTA1NjkwMTIyMzI3Ng%3D%3D.2"
  //     }
  //     return user;
  // }

  updateUser(user: User): Promise<any> {

    return this.dataService.putFullRequest<User>('users/' + user.Id, user)
    .catch(err => location.href = 'index');
  }
  getRating(id: number): Promise<any> {
    return this.dataService.getFullRequest<number>('users/'+id+'/rating')
    .catch(err => location.href = 'index');
  }
  getReviews(id: number): Promise<any> {
    return this.dataService.getFullRequest<Review[]>('users/' + id + '/reviews')
      .catch(err => location.href = 'index');
  }

}
