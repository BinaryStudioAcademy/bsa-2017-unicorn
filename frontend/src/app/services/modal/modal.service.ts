import { Injectable } from '@angular/core';
import { DataService } from ".././data.service";
import { environment } from "../../../environments/environment";
import { SuiModule } from 'ng2-semantic-ui';
import { ImageCropperComponent, CropperSettings } from 'ng2-img-cropper';

import {
  SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal
} from 'ng2-semantic-ui';

@Injectable()
export class ModalService {

  imageUploaded: boolean;
  cropperSettings: CropperSettings;
  private activeModal: SuiActiveModal<void, {}, void>;

  constructor(private dataService: DataService, private suiModalService: SuiModalService) {
    dataService.setHeader('Content-Type', 'application/json');

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

  fileChange(file: File, cropper: ImageCropperComponent) {
    var image: any = new Image();
    var myReader: FileReader = new FileReader();
    var that = this;
    myReader.onloadend = function (loadEvent: any) {
      image.src = loadEvent.target.result;
      cropper.setImage(image);

    };
    this.imageUploaded = true;
    myReader.readAsDataURL(file);
  }

  public openModal(modalTemplate: ModalTemplate<void, {}, void>, size = ModalSize.Normal): SuiActiveModal<void, {}, void> {
    const config = new TemplateModalConfig<void, {}, void>(modalTemplate);
    //config.closeResult = "closed!";

    config.size = size;
    config.isInverted = true;
    //config.mustScroll = true;
    let that = this;

    return this.suiModalService
      .open(config)
      .onApprove(result => { /* approve callback */ })
      .onDeny(result => {
        this.imageUploaded = false;
      });
  }

}
