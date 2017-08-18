import { Injectable } from '@angular/core';
import { DataService } from "./data.service";
import { environment } from "../../environments/environment";
import {SuiModule} from 'ng2-semantic-ui';
import {ImageCropperComponent, CropperSettings} from 'ng2-img-cropper';

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

export interface IContext { }

@Injectable()
export class CropService { 

  imageUploaded: boolean;  

  constructor(private dataService: DataService) {
    dataService.setHeader('Content-Type', 'application/json');
   }

   fileChange(file: File, cropper:ImageCropperComponent) {
        var image:any = new Image();
        var myReader:FileReader = new FileReader();
        var that = this;
        myReader.onloadend = function (loadEvent:any) {
            image.src = loadEvent.target.result;
            cropper.setImage(image);

        };
        this.imageUploaded = true;
        myReader.readAsDataURL(file);
    }

    public openModal(modalTemplate: ModalTemplate<IContext, string, string>) {
    const config = new TemplateModalConfig<IContext, string, string>(modalTemplate);
    //config.closeResult = "closed!";
    
    config.context = {};
    config.size = ModalSize.Normal;
    config.isInverted = true;
    //config.mustScroll = true;
    let that = this;

    // this.activeModal = this.modalService
    //   .open(config)
    //   .onApprove(result => { /* approve callback */ })
    //   .onDeny(result => {
    //     that.imageUploaded = false;
    //   });
  }

}
