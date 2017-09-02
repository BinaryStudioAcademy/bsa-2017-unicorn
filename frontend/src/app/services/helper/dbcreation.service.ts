import { Injectable } from '@angular/core';
import { DataService } from '../data.service';

@Injectable()
export class DbcreationService {

  constructor(private httpService: DataService) { }

  public RecreateDatabase() {
    return this.httpService.getFullRequest('db');
  }
}
