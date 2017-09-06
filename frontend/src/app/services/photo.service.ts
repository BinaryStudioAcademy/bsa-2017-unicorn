import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Headers, RequestOptions, Response, Http } from '@angular/http';
import { imgurenvironment } from '../../environments/environment';
import { PhotoutilService } from '../services/photoutil.service';
import { Observable, Subject } from 'rxjs';

import { DataService } from '../services/data.service';
import { FileReaderUtils } from './file-utils';
import { TokenHelperService } from './helper/tokenhelper.service';


export type ImgurUploadOptions = {
    clientId: string,
    imageData: Blob | string,
    title?: string
};

export type ImgurUploadResponse = {
    data?: {
        link: string,
        deleteHash: string
    },
    success: boolean
};

@Injectable()
export class PhotoService {
    constructor(
        private http: Http, 
        private dataService: DataService,
        private tokenHelper: TokenHelperService){
        this.dataService.setHeader('Content-Type', 'application/json');
    }

    uploadToImgur(image: Blob | string): Promise<string> {
        if (!this.tokenHelper.isTokenValid()) {
            return Promise.reject('Not authenticated');
        }
        let up = new Ng2ImgurUploader(this.http);        
        let op: ImgurUploadOptions = {
            clientId: imgurenvironment.client_id,
            title: 'title',
            imageData: image
        };
        return up.upload(op).toPromise().then(resp => resp.data.link);
    }

    private getId(): string {
        console.log(this.tokenHelper.getAllClaims());
        return this.tokenHelper.getClaimByName('accountid');
    }

    saveBanner(imageUrl: string): Promise<string> {
        console.log('savebanner');
        return this.dataService.postFullRequest(`background/${this.getId()}`, imageUrl)
            .then(() => {return imageUrl;});
    }

    saveAvatar(imageUrl: string): Promise<any> {
        console.log('saveavatar');
        return this.dataService.postFullRequest(`avatar/${this.getId()}`, imageUrl)
            .then(() => {return imageUrl;});
    }

    saveCroppedAvatar(imageUrl: string): Promise<any> {
        console.log('saveavatar');
        return this.dataService.postFullRequest(`avatar/cropped/${this.getId()}`, imageUrl)
            .then(() => {return imageUrl;});
    }
}

@Injectable()
export class Ng2ImgurUploader {
    constructor(
        private http: Http
    ) { }

    upload(uploadOptions: ImgurUploadOptions) {
        let result = new Subject<ImgurUploadResponse>();
        if(typeof uploadOptions.imageData === 'string'){
            this.sendImgurRequest(uploadOptions.imageData, uploadOptions, result);
        }
        else{
        FileReaderUtils.imageDataToBase64(uploadOptions.imageData as Blob)
            .subscribe(
                (imageBase64: string) => {                    
                    this.sendImgurRequest(imageBase64, uploadOptions, result);
                },
                (error: string) => {
                    result.error(error);
                }
            ); 
        }  

        return result;
    }

    delete(clientId: string, deleteHash: string): Observable<string> {
        let options = this.buildRequestOptions(clientId);
        return this.http.delete(`https://api.imgur.com/3/image/${deleteHash}`, options)
            .map((res: Response) => res.text());
    }

    private buildRequestOptions(clientId) {
        let headers = new Headers({
            Authorization: 'Client-ID ' + clientId,
            Accept: 'application/json'
        });
        return new RequestOptions({headers: headers});
    }

    private sendImgurRequest(
        imageBase64: string,
        uploadOptions: ImgurUploadOptions,
        result: Subject<ImgurUploadResponse>
    ): Observable<ImgurUploadResponse> {
        let options = this.buildRequestOptions(uploadOptions.clientId);
        let body = {
            image: imageBase64,
            title: uploadOptions.title,
            type: 'base64'
        };

        this.http.post('https://api.imgur.com/3/image', body, options)
            .subscribe(
                (res: Response) => {
                    let responseData = res.json().data;
                    //console.log(responseData.link);
                    result.next({
                        data: {
                            link: responseData.link,
                            deleteHash: responseData.deletehash
                        },
                        success: true
                    });
                    result.complete();
                },
                (err: Response) => {
                    result.error('error uploading image: ' + err.text());
                }
            );

        return result;
    }
}