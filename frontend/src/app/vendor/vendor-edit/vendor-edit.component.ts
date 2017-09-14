import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser'

import { VendorEditChartsComponent } from './vendor-edit-charts/vendor-edit-charts.component';

import {SuiModule} from 'ng2-semantic-ui';

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

import {ImageCropperComponent, CropperSettings} from 'ng2-img-cropper';

import 'rxjs/add/operator/switchMap';

import { Vendor } from '../../models/vendor.model';

import { VendorService } from "../../services/vendor.service";
import { PhotoService } from '../../services/photo.service';
import { ModalService } from "../../services/modal/modal.service";
import { MenuEventsService } from "../../services/events/menu-events.service";

@Component({
  selector: 'app-vendor-edit',
  templateUrl: './vendor-edit.component.html',
  styleUrls: ['./vendor-edit.component.sass'],
  providers: [ModalService]
})
export class VendorEditComponent implements OnInit {
  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<void, {}, void>;
  private activeModal: SuiActiveModal<void, {}, void>;

  @ViewChild('cropper', undefined)
  cropper: ImageCropperComponent;

  @ViewChild(VendorEditChartsComponent)
  charts: VendorEditChartsComponent;

  vendor: Vendor;

  uploading: boolean;
  backgroundUrl: SafeResourceUrl;

  file: File;
  data: any;
  imageUploaded: boolean;
  cropperSettings: CropperSettings;
  dataLoaded: boolean;

  messagesTabActive: boolean;
  worksTabActive: boolean;

  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService,
    private photoService: PhotoService,
    private modalService: ModalService,
    private sanitizer: DomSanitizer,
    private menuEventsService: MenuEventsService
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
        this.vendor.Birthday = new Date(this.vendor.Birthday);
        this.backgroundUrl = this.buildSafeUrl(this.vendor.Background != null ? this.vendor.Background : "https://www.beautycolorcode.com/d8d8d8.png");
      });
      if (this.route.snapshot.queryParams['tab'] === 'messages') {
        this.messagesTabActive = true;
      }
      if (this.route.snapshot.queryParams['tab'] === 'works') {
        this.worksTabActive = true;
      } 
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
    var image: any = new Image();
    this.file = $event.target.files[0];
    var myReader: FileReader = new FileReader();
    var that = this;
    myReader.onloadend = function (loadEvent: any) {
      image.src = loadEvent.target.result;
      that.cropper.setImage(image);

    };
    this.imageUploaded = true;
    myReader.readAsDataURL(this.file);
  }

  fileSaveListener() {
    //debugger;
    if (!this.data) {
      console.log("file not upload");
      return;
    }
    if (!this.file) {
      console.log('file null');
      return;
    }
    this.dataLoaded = false;
    let mass = [];
    mass.push(this.photoService.uploadToImgur(this.file)
              .then(res => {
                  this.photoService.saveAvatar(res);
                  return res;
                }));
    mass.push(this.photoService.uploadToImgur(this.data.image.split('base64,')[1])
              .then(res => this.photoService.saveCroppedAvatar(res)));
    Promise.all(mass).then(values => {
      this.vendor.CroppedAvatar = this.data.image;
      this.menuEventsService.croppedAvatar(this.data.image);  
      this.menuEventsService.changedAvatar(values[0]); 
      this.dataLoaded = true;
      this.imageUploaded = false;
      this.activeModal.deny(null);
    }).catch(err => console.log(err));
    //console.log(this.data);
    //console.log(this.data.image);
  }

  public openModal() {
    this.imageUploaded = false;
    this.activeModal = this.modalService.openModal(this.modalTemplate);
  }

  activateCharts() {
    this.charts.enabled = true;
  }
}