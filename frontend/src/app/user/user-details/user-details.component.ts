import { Component, OnInit,Input,OnDestroy, ViewChild } from '@angular/core';
import { FormsModule }   from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import {SuiModule} from 'ng2-semantic-ui';
import { ActivatedRoute, Params, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { User } from '../../models/user';

import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { UserService } from "../../services/user.service";
import { ModalService } from "../../services/modal/modal.service";
import { PhotoService, Ng2ImgurUploader } from '../../services/photo.service';
import {ImageCropperComponent, CropperSettings} from 'ng2-img-cropper';

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';

export interface IContext { }

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.sass'],
  providers: [
        PhotoService,
        Ng2ImgurUploader,
        ModalService]
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
  backgroundUrl: SafeResourceUrl;
  uploading: boolean;
  isOwner: boolean;
  user:User;

  modalSize: string;
  cropperSettings: CropperSettings;
  data: any;
  file: File;
  imageUploaded: boolean;  

  constructor( 
    private route: ActivatedRoute,
    private userService: UserService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer,
    private suiModalService: SuiModalService,
    private modalService: ModalService,    
    private tokenHelper: TokenHelperService) { 
      this.cropperSettings = modalService.cropperSettings;
      this.data = {};
      this.imageUploaded = false;
  }
  ngOnInit() {

    this.initOwnerParam();
    this.route.params
    .switchMap((params: Params) => this.userService.getUser(params['id']))
    .subscribe(resp => {
      this.user = resp.body as User;
      this.backgroundUrl = this.buildSafeUrl(this.user.Background);
    });
  }

  initOwnerParam(): void {
    let id = this.route.snapshot.paramMap.get('id');
    if (this.tokenHelper.getClaimByName('id') === id) {
      console.log('owner');
      this.isOwner = true;
    }
    console.log(id);
    console.log(this.tokenHelper.getClaimByName('id'));
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
        this.activeModal.deny('');        
        this.user.Avatar = this.data.image;
      })
      .catch(err => console.log(err));
    }).catch(err => {
      console.log(err);
    });
  }

  public openModal() {
    debugger;
    this.modalService.openModal(this.modalTemplate, this.activeModal);
  }
}