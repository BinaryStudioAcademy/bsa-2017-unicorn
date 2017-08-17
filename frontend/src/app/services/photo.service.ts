import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Headers, RequestOptions, Response, Http } from '@angular/http';
import { imgurenvironment } from '../../environments/environment';
import { PhotoutilService } from '../services/photoutil.service';
import { Observable, Subject } from 'rxjs';

import { DataService } from '../services/data.service';
import { FileReaderUtils } from './file-utils';


export type ImgurUploadOptions = {
    clientId: string,
    imageData: Blob,
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
    constructor(private http: Http, private dataService: DataService){
        this.dataService.setHeader('Content-Type', 'application/json');
    }

    uploadToImgur(image: Blob): Promise<ImgurUploadResponse> {
        let up = new Ng2ImgurUploader(this.http);
        let op: ImgurUploadOptions = {
            clientId: imgurenvironment.client_id,
            title: 'title',
            imageData: image
        };
        return up.upload(op).toPromise();
    }

    saveBanner(imageUrl: string): Promise<any> {
        console.log('savebanner');
        return this.dataService.postFullRequest('background/11', imageUrl);
    }

    saveAvatar(imageUrl: string): Promise<any> {
        console.log('saveavatar');
        return this.dataService.postFullRequest('avatar/11', imageUrl);
    }
}

@Injectable()
export class Ng2ImgurUploader {
    constructor(
        private http: Http
    ) { }

    upload(uploadOptions: ImgurUploadOptions) {
        let result = new Subject<ImgurUploadResponse>();

        FileReaderUtils.imageDataToBase64(uploadOptions.imageData)
            .subscribe(
                (imageBase64: string) => {
                    this.sendImgurRequest(imageBase64, uploadOptions, result);
                },
                (error: string) => {
                    result.error(error);
                }
            );

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






// export type ImgurUploadOptions = {
//   clientId: string,
//   imageData: Blob,
//   title?: string
// };

// export type ImgurUploadResponse = {
//   data?: {
//       link: string,
//       deleteHash: string
//   },
//   success: boolean
// };


// @Injectable()
// export class PhotoService {

//   refresh_token: string = imgurenvironment.refresh_token;
//   client_id: string = imgurenvironment.client_id;
//   client_secret: string = imgurenvironment.client_secret;
//   url: string;

//   upload(uploadOptions: ImgurUploadOptions) {
//     let result = new Subject<ImgurUploadResponse>();

//     PhotoutilService.imageDataToBase64(uploadOptions.imageData)
//         .subscribe(
//             (imageBase64: string) => {
//                 this.sendImgurRequest(imageBase64, uploadOptions, result);
//             },
//             (error: string) => {
//                 result.error(error);
//             }
//         );

//     return result;
// }

//   constructor(private dataService: DataService) {
//   }  

//   private buildRequestOptions(clientId) {
//     let headers = new Headers({
//         Authorization: 'Client-ID ' + clientId,
//         Accept: 'application/json'
//     });
//     return new RequestOptions({headers: headers});
// }

// delete(clientId: string, deleteHash: string): Observable<string> {
//   let options = this.buildRequestOptions(clientId);
//   return this.dataService.deleteFullRequest(`https://api.imgur.com/3/image/${deleteHash}`, options).then(x =>(res: Response) => res.text() );
// }

//   private sendImgurRequest(
//     imageBase64: string,
//     uploadOptions: ImgurUploadOptions,
//     result: Subject<ImgurUploadResponse>
// ): Observable<ImgurUploadResponse> {
//     let options = this.buildRequestOptions(uploadOptions.clientId);
//     let body = {
//         image: imageBase64,
//         title: uploadOptions.title,
//         type: 'base64'
//     };
    
//     this.dataService.postFullRequest<string>('https://api.imgur.com/3/image', body).then(
//       request=>  (res: Response) => {
//       let responseData = res.json().data;
//       result.next({
//           data: {
//               link: responseData.link,
//               deleteHash: responseData.deletehash
//           },
//           success: true
//       });
//       result.complete();
//   },
//   (err: Response) => {
//       result.error('error uploading image: ' + err.text());
//   }) ;

//   return result;
//   }


// }
