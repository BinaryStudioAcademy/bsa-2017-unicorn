import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { User } from '../models/user'

@Injectable()
export class UserService {

  constructor(private dataService: DataService) { }

 
  getUser(id: number) : User {
    var user: User = new User();

    user.id = 0;
    user.firstName = "Vasya";
    user.lastName = "Pupkin";
    user.location = "Dnipro";
    user.avatarUrl = "http://scontent-sea1-1.cdninstagram.com/t51.2885-15/s480x480/e35/10349706_559451967536913_1168844394_n.jpg?ig_cache_key=MTA1NTAyNTA1NjkwMTIyMzI3Ng%3D%3D.2";
    
    return user;
  }

}
