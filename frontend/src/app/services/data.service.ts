import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { TokenHelperService } from "./helper/tokenhelper.service";

import 'rxjs/add/operator/toPromise';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

@Injectable()
export class DataService {

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
    this.setHeader('Authorization','Bearer ' + this.tokenHelper.getToken());
    return new HttpHeaders(this.headers);
  }

  prepareData(payload: Object): string {
    return JSON.stringify(payload);
  }

  constructor(private http: HttpClient, private tokenHelper: TokenHelperService) { }

  getData() {
    return this.http.get(environment.apiUrl + 'values');
  }

  getRequest<T>(url: string): Promise<T> {
    return this.http
      .get<T>(this.buildUrl(url), { headers: this.getHeaders() })
      .toPromise()
      .catch(this.handleError);
  }

  getFullRequest<T>(url: string): Promise<T> {
    return this.http
      .get<T>(this.buildUrl(url), { observe: 'response',  headers: this.getHeaders() })
      .toPromise()
      .catch(this.handleError);
  }

  postClearRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .post<T>(this.buildUrl(url), payload)
      .toPromise()
      .catch(this.handleError);
  }

  postRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .post<T>(this.buildUrl(url), this.prepareData(payload), { headers: this.getHeaders() })
      .toPromise()
      .catch(this.handleError);
  }

  postFullRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .post<T>(this.buildUrl(url), this.prepareData(payload), { headers: this.getHeaders(), observe: 'response' })
      .toPromise()
      .catch(this.handleError);
  }

  putRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .put<T>(this.buildUrl(url), this.prepareData(payload), { headers: this.getHeaders() })
      .toPromise()
      .catch(this.handleError);
  }

  putFullRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .put<T>(this.buildUrl(url), this.prepareData(payload), { headers: this.getHeaders(), observe: 'response' })
      .toPromise()
      .catch(this.handleError);
  }

  deleteRequest<T>(url: string): Promise<T> {
    return this.http
      .delete<T>(this.buildUrl(url), { headers: this.getHeaders() })
      .toPromise()
      .catch(this.handleError);
  }

  deleteFullRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .delete<T>(this.buildUrl(url), { headers: this.getHeaders(), observe: 'response' })
      .toPromise()
      .catch(this.handleError);
  }

  patchRequest<T>(url: string, payload: Object): Promise<T> {
    return this.http
      .patch<T>(this.buildUrl(url), this.prepareData(payload), { headers: this.getHeaders() })
      .toPromise()
      .catch(this.handleError);
  }

  handleError(error: any): Promise<any> {
    if (!environment.production) {
      console.log(error);
    }
    return Promise.reject(error);
  }
}
