import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';
import { JwtHelper } from '../../helpers/jwthelper';
import 'rxjs/add/operator/switchMap';

import {SuiModule} from 'ng2-semantic-ui';

import { Vendor } from '../../models/vendor.model';

import { VendorService } from "../../services/vendor.service";
import { ModalService } from "../../services/modal/modal.service";

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

import {ImageCropperComponent, CropperSettings} from 'ng2-img-cropper';
import { PhotoService, Ng2ImgurUploader } from "../../services/photo.service";

import { FormsModule }   from '@angular/forms';

export interface IContext { }

@Component({
  selector: 'app-vendor-details',
  templateUrl: './vendor-details.component.html',
  styleUrls: ['./vendor-details.component.sass'],
  providers: [
        PhotoService,
        Ng2ImgurUploader,
        ModalService]
})
export class VendorDetailsComponent implements OnInit {

  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<IContext, string, string>;
  private activeModal: SuiActiveModal<IContext, {}, string>;

  @ViewChild('cropper', undefined)
  cropper:ImageCropperComponent;
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton:boolean = false;
  backgroundUrl: SafeResourceUrl;
  uploading: boolean;
  isOwner: boolean;

  cropperSettings: CropperSettings;
  vendor: Vendor;
  isGuest: boolean;
  file: File;
  data: any;
  imageUploaded: boolean;
  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService,
    private modalService: ModalService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer
  ) {
    this.getCurrentRole();
    this.cropperSettings = modalService.cropperSettings;
    this.data = {};
    this.imageUploaded = false;
  }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.vendorService.getVendor(params['id']))
      .subscribe(resp => {
        this.vendor = resp.body as Vendor
      });
  }
  getCurrentRole()
  {
    let token = localStorage.getItem('token');
    if(token===null)
     { 
       this.isGuest=true;
       return;
     }
    const userClaims = new JwtHelper().decodeToken(token);
    if(userClaims['roleid']!=1)
        this.isGuest=false; else
    this.isGuest=true;
  }

  buildSafeUrl(link: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustStyle(`url('${link}')`);
  }
  
  bannerListener($event) {
      let file: File = $event.target.files[0];
      this.uploading = true;
      this.photoService.uploadToImgur(file).then(link => {
        console.log(link);
        return this.photoService.saveBanner(link);
      }).then(link => {
        this.backgroundUrl = this.buildSafeUrl(link);
        this.uploading = false;
      }).catch(err => {
        console.log(err);
        this.uploading = false;
      });
  }


  fileChangeListener($event) {
    var image:any = new Image();
    this.file = $event.target.files[0];
    var myReader:FileReader = new FileReader();
    var that = this;
    myReader.onloadend = function (loadEvent:any) {
        image.src = loadEvent.target.result;
        that.cropper.setImage(image);

    };
    this.imageUploaded = true;
    myReader.readAsDataURL(this.file);
}

  fileSaveListener(){
    if (!this.data)
    {
      console.log("file not upload");
      return;
    }

    this.photoService.uploadToImgur(this.file).then(resp => {

      let path = resp;
      console.log(path);
      this.photoService.saveAvatar(path)
      .then(resp => {
        this.vendor.Avatar = this.data.image;    
        this.activeModal.deny('');    
      })
      .catch(err => console.log(err));
    }).catch(err => {
      console.log(err);
    });
  }

  public openModal() {
    this.activeModal = this.modalService.openModal(this.modalTemplate);
  }
}
