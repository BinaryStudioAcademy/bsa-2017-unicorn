import { Component, OnInit,Input,OnDestroy, ViewChild } from '@angular/core';
import { FormsModule }   from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';
import { ActivatedRoute, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { User } from '../../models/user';

import { UserService } from "../../services/user.service";
import { PhotoService, Ng2ImgurUploader } from '../../services/photo.service';
import {ImageCropperComponent, CropperSettings} from 'ng2-img-cropper';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.sass'],
  providers: [
        PhotoService,
        Ng2ImgurUploader]
})
export class UserDetailsComponent implements OnInit {
  
@ViewChild('cropper', undefined)
 cropper:ImageCropperComponent;
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton:boolean = false;
  user:User;

  cropperSettings: CropperSettings;
  data: any;
  constructor( 
    private route: ActivatedRoute,
    private userService: UserService,
    private photoService: PhotoService) { 
     this.cropperSettings = new CropperSettings();
        this.cropperSettings.width = 100;
        this.cropperSettings.height = 100;
        this.cropperSettings.croppedWidth =100;
        this.cropperSettings.croppedHeight = 100;
        this.cropperSettings.canvasWidth = 400;
        this.cropperSettings.canvasHeight = 300;
        
        this.cropperSettings.noFileInput = true;
        this.cropperSettings.rounded = true;
        
        this.data = {};
  }
  ngOnInit() {
    // this.fakeUser = this.userService.getUser(0);
    this.userService.getUser(3).then(res => {
      this.user = res;      
      console.log(this.user);
      });     
  }
 openModal() {
    this.enabled = true;
  }
 updateBg(color:string)
 {
    document.getElementById("user-header").style.backgroundColor = color;
 }

 bannerListener($event) {
    let file: File = $event.target.files[0];
    this.photoService.uploadToImgur(file).then(resp => {
      let path = resp.data.link;
      console.log(path);
      this.photoService.saveBanner(path)
      .then(resp => {
        document.getElementById("user-header").style.backgroundImage = `url('${path}')`;
      })
      .catch(err => console.log(err));
    }).catch(err => {
      console.log(err);
    });
 }

  fileChangeListener($event) {
    var image:any = new Image();
    var file:File = $event.target.files[0];
    var myReader:FileReader = new FileReader();
    var that = this;
    myReader.onloadend = function (loadEvent:any) {
        image.src = loadEvent.target.result;
        that.cropper.setImage(image);

    };

    myReader.readAsDataURL(file);
}
}