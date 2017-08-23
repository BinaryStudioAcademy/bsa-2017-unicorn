import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser'

import {SuiModule} from 'ng2-semantic-ui';

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

import {ImageCropperComponent, CropperSettings} from 'ng2-img-cropper';

import 'rxjs/add/operator/switchMap';

import { Vendor } from '../../models/vendor.model';

import { VendorService } from "../../services/vendor.service";
import { PhotoService } from '../../services/photo.service';
import { ModalService } from "../../services/modal/modal.service";

export interface IContext { }

@Component({
  selector: 'app-vendor-edit',
  templateUrl: './vendor-edit.component.html',
  styleUrls: ['./vendor-edit.component.sass'],
  providers: [ModalService]
})
export class VendorEditComponent implements OnInit {
    @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<IContext, string, string>;
  private activeModal: SuiActiveModal<IContext, {}, string>;

  @ViewChild('cropper', undefined)
  cropper:ImageCropperComponent;

  vendor: Vendor;

  uploading: boolean;
  backgroundUrl: SafeResourceUrl;

  file: File;
  data: any;
  imageUploaded: boolean;
  cropperSettings: CropperSettings;
  dataLoaded: boolean;

  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService,
    private photoService: PhotoService,
    private modalService: ModalService,
    private sanitizer: DomSanitizer
  ) { 
    this.cropperSettings = modalService.cropperSettings;
      this.data = {};
      this.imageUploaded = false;
  }

  ngOnInit() {
    this.dataLoaded = true;
    this.route.params
      .switchMap((params: Params) => this.vendorService.getVendor(params['id']))
      .subscribe(resp => {
        this.vendor = resp.body as Vendor;
        this.backgroundUrl = this.buildSafeUrl(this.vendor.Background);
      });
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
    this.dataLoaded = false;
    this.photoService.uploadToImgur(this.file).then(resp => {

      let path = resp;
      console.log(path);
      this.photoService.saveAvatar(path)
      .then(resp => {
        this.vendor.Avatar = this.data.image;
        this.dataLoaded = true;  
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