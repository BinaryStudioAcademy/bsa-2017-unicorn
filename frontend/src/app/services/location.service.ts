import { Injectable } from '@angular/core';

import { DataService } from './data.service';

import { Location } from '../models/location.model';

import { Observable, Observer } from 'rxjs';
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
    
	getLocDetails(lat: number, lng: number): Observable<any>
    {
        let geocoder = new google.maps.Geocoder();
        var latlng = {lat: lat, lng: lng}
        return Observable.create(observer => {
            geocoder.geocode( { 'location':latlng }, function(results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    observer.next(results[0]);
                    observer.complete();
                } else {
                    console.log('Error - ', results, ' & Status - ', status);
                    observer.next({});
                    observer.complete();
                }
            });
        })
    }

    getCurrentLocation() {
        var location = new Location();
         if (navigator.geolocation)
          {
            navigator.geolocation.getCurrentPosition(position => {
            location.Latitude = position.coords.latitude || 49.841459;
            location.Longitude = position.coords.longitude || 24.031946;
            
           });
         } else 
      {
        location.Latitude = 49.841459;
        location.Longitude = 24.031946;
      }
      return location;
    }

    getFullLocation()
    {
       
    }
}