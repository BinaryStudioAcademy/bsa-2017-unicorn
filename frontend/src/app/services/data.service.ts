import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class DataService {

  //urll = /^((?:https:\/\/)|(?:http:\/\/)|(?:www))/;

  

  baseUrl: string = environment.apiUrl;

  headers: any = {};

  setHeader(key: string, value: string): void {
    this.headers[key] = value;
  }

  getHeader(key: string): string {
    return this.headers[key];
  }

  deleteHeader(key: string): void {
    delete this.headers[key];
  }

  setBaseUrl(url: string): void {
    this.baseUrl = url;
  }

  buildUrl(url: string): string {
    if (url.startsWith('http://') || url.startsWith('https://')) {
      return url;
    }
    return this.baseUrl + url;
  }

  getHeaders(): HttpHeaders {
    return new HttpHeaders(this.headers);
  }

  prepareData(payload: Object): string {
    return JSON.stringify(payload);
  }

  constructor(private http: HttpClient) { }

  getData() {
    return this.http.get(environment.apiUrl + 'values');
  }

  getRequest<T>(url: string): Promise<T> {
    return this.http
      .get<T>(this.buildUrl(url))
      .toPromise()
      ;
  }

  postRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .post<T>(this.buildUrl(url), this.prepareData(payload), {headers: this.getHeaders()})
      .toPromise();
  }

  putRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .put<T>(this.buildUrl(url), this.prepareData(payload), {headers: this.getHeaders()})
      .toPromise();
  }

  deleteRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .delete<T>(this.buildUrl(url), {headers: this.getHeaders()})
      .toPromise();
  }

  patchRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .patch<T>(this.buildUrl(url), this.prepareData(payload), {headers: this.getHeaders()})
      .toPromise();
  }
}
