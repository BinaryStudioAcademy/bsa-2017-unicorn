import { Component, OnInit,Input,OnDestroy, ViewChild } from '@angular/core';
import { FormsModule }   from '@angular/forms';

import {SuiModule} from 'ng2-semantic-ui';
import { ActivatedRoute, Params } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { User } from '../../models/user';

import { UserService } from "../../services/user.service";
import {ImageCropperComponent, CropperSettings} from 'ng2-img-cropper';

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

export interface IContext { }

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.sass']
})
export class UserDetailsComponent implements OnInit {
  
  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<IContext, string, string>;
  private activeModal: SuiActiveModal<IContext, {}, string>;

@ViewChild('cropper', undefined)
 cropper:ImageCropperComponent;
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton:boolean = false;
  fakeUser:User;

  modalSize: string;


  cropperSettings: CropperSettings;
  data: any;
  constructor( private route: ActivatedRoute,
    private userService: UserService,
    public modalService: SuiModalService) { 
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
    this.fakeUser = this.userService.getUser(0);
  }

 updateBg(color:string)
 {
    document.getElementById("user-header").style.backgroundColor = color;
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
  private closeModal() {
    this.activeModal.deny(''); 
  }

  public openModal() {
    const config = new TemplateModalConfig<IContext, string, string>(this.modalTemplate);
    config.closeResult = "closed!";
    config.context = {};
    config.size = ModalSize.Normal;
    config.isInverted = true;
    //config.mustScroll = true;

    this.activeModal = this.modalService
      .open(config)
      .onApprove(result => { /* approve callback */ })
      .onDeny(result => {
        
      });
  }
}