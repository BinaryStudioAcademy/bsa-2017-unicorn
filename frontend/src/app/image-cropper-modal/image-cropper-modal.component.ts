import { Component, OnInit, ViewChild, ElementRef, AfterContentInit } from '@angular/core';

import { SuiModal, ComponentModalConfig, ModalSize, ModalTemplate } from "ng2-semantic-ui"
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';
import { CropperSettings, ImageCropperComponent } from 'ng2-img-cropper';
import { PhotoService, Ng2ImgurUploader } from '../services/photo.service';

interface ICropperModalContext {
  image: string;
}

export class ImageCropperModal extends ComponentModalConfig<ICropperModalContext, string, void> {
  constructor() {
      super(ImageCropperModalComponent);

      this.isClosable = true;
      this.isInverted = true;
      this.size = "normal";
  }
}

@Component({
  selector: 'app-image-cropper-modal',
  templateUrl: './image-cropper-modal.component.html',
  styleUrls: ['./image-cropper-modal.component.sass'],
  providers: [
    PhotoService,
    Ng2ImgurUploader
  ]
})
export class ImageCropperModalComponent implements OnInit {
  public modalTemplate: ModalTemplate<ICropperModalContext, string, void>;
  
  @ViewChild('cropper', undefined)
  cropper: ImageCropperComponent;
  
  cropperSettings: CropperSettings;
  data: any;
  file: File;
  isFileUploading: boolean;

  @ViewChild('fileInput') fileInput: ElementRef;
  @ViewChild('defaultImage') defaultImage: ElementRef;

  constructor(
    public modal: SuiModal<ICropperModalContext, string, void>,
    private sanitizer: DomSanitizer,
    private photoService: PhotoService
  ) {
    this.data = {};

    this.cropperSettings = new CropperSettings();
    this.cropperSettings.width = 100;
    this.cropperSettings.height = 100;
    this.cropperSettings.croppedWidth = 140;
    this.cropperSettings.croppedHeight = 140;
    this.cropperSettings.canvasWidth = 400;
    this.cropperSettings.canvasHeight = 300;
    this.cropperSettings.noFileInput = true;
    this.cropperSettings.rounded = true;
   }

  ngOnInit() {
    this.cropper.settings = this.cropperSettings;
  }

  uploadFile(): void {
    let input: HTMLElement = this.fileInput.nativeElement as HTMLElement;
    input.click();
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
    myReader.readAsDataURL(this.file);  
  }

  onImageSave(): void {
    if (this.file && this.data) {
      this.isFileUploading = true;
      var cropped = this.data.image.split('base64,')[1];
      this.photoService.uploadToImgur(cropped)
        .then(resp => {
          this.isFileUploading = false;
          this.modal.approve(resp);
        })
        .catch(err => {
          this.isFileUploading = false;
          console.log(err);
        });
    }
  }
}
