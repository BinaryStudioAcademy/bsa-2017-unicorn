import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Location } from '../models/location.model';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class LocationService {
	private apiController: string

	constructor(private dataService: DataService) {
		this.apiController = "location";
	}

	getById(id: number): Promise<any> {
		return this.dataService.getFullRequest<Location>(`${this.apiController}/${id}`);
	}

}