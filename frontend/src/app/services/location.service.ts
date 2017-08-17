import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Location } from '../models/location.model';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class LocationService {
	private resourceUrl: string

	constructor(private dataService: DataService) {
		this.resourceUrl = dataService.buildUrl("location");
	}

	getById(id: number): Promise<Location> {
		return this.dataService.getRequest<Location>(`${this.resourceUrl}/${id}`);
	}

}