import { Component, OnInit, Input, OnDestroy, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import { SuiModule } from 'ng2-semantic-ui';
import { ActivatedRoute, Params, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { User } from '../../models/user';

import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { UserService } from "../../services/user.service";
import { ModalService } from "../../services/modal/modal.service";
import { PhotoService, Ng2ImgurUploader } from '../../services/photo.service';
import { ImageCropperComponent, CropperSettings } from 'ng2-img-cropper';

import {
  SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal
} from 'ng2-semantic-ui';
import { ToastsManager, Toast } from 'ng2-toastr';

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
  public modalTemplate: ModalTemplate<void, {}, void>;
  private activeModal: SuiActiveModal<void, {}, void>;

  @ViewChild('cropper', undefined)
  cropper: ImageCropperComponent;
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton: boolean = false;
  backgroundUrl: SafeResourceUrl;
  uploading: boolean;
  isOwner: boolean;
  user: User;
  dataLoaded: boolean;

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
    private tokenHelper: TokenHelperService,
    public toastr: ToastsManager) { 
      this.cropperSettings = modalService.cropperSettings;
      this.data = {};
      this.imageUploaded = false;
  }
  ngOnInit() {
    this.dataLoaded = true;
    this.route.params
      .switchMap((params: Params) => this.userService.getUser(params['id']))
      .subscribe(resp => {
        this.user = resp.body as User;
        this.backgroundUrl = this.buildSafeUrl(this.user.Background);
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
      this.toastr.success('Your background was updated', 'Success!');
    }).catch(err => {
      console.log(err);
      this.uploading = false;
      this.toastr.error('Sorry, something went wrong', 'Error!');
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
    if (!this.data) {
      console.log("file not upload");
      this.toastr.error('You have to pick photo', 'Error!');
      return;
    }
    if (!this.file) {
      return;
    }
    this.dataLoaded = false;
    this.photoService.uploadToImgur(this.file).then(resp => {

      let path = resp;
      console.log(path);
      this.photoService.saveAvatar(path)
        .then(resp => {
          this.user.Avatar = this.data.image;
          this.dataLoaded = true;
          this.toastr.success('Your avatar was updated', 'Success!');
          this.activeModal.deny(null);
        })
        .catch(err => {
          console.log(err);
          this.toastr.error('Sorry, something went wrong', 'Error!');
          this.activeModal.deny(null);
        });
    }).catch(err => {
      console.log(err);
      this.toastr.error('Sorry, something went wrong', 'Error!');
      this.activeModal.deny(null);
    });
  }

  public openModal() {
    this.activeModal = this.modalService.openModal(this.modalTemplate);
  }
}