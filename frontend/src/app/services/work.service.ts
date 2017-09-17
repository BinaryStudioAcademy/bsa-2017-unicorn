import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Work } from "../models/work.model";

@Injectable()
export class WorkService {
  private apiController: string;

  constructor(private dataService: DataService) 
  { 
    dataService.setHeader('Content-Type', 'application/json');
    this.apiController = "work";
  }

  getAll() : Promise<any> {
	return this.dataService.getFullRequest<Work[]>(this.apiController)
		.catch(err => console.log(err));
  }
}