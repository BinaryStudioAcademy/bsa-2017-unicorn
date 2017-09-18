import { Component, OnInit, Input, OnDestroy, ViewChild } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

import { SuiModule } from 'ng2-semantic-ui';
import { ActivatedRoute, Params, ParamMap } from '@angular/router';
import 'rxjs/add/operator/switchMap';

import { User } from '../../models/user';

import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { UserService } from "../../services/user.service";
import { PhotoService, Ng2ImgurUploader } from '../../services/photo.service';
import { ImageCropperComponent, CropperSettings } from 'ng2-img-cropper';
import { Subscription } from "rxjs/Subscription";
import { UnreadDialogsService } from "../../services/chat/unread-dialogs.service";

import {
  SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal
} from 'ng2-semantic-ui';
import { ToastsManager, Toast } from 'ng2-toastr';
import { MenuEventsService } from "../../services/events/menu-events.service";
import { ChatEventsService } from "../../services/events/chat-events.service";
import { ImageCropperModal } from '../../image-cropper-modal/image-cropper-modal.component';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.sass'],
  providers: [
    PhotoService,
    Ng2ImgurUploader
  ]
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

  tasksTabActive: boolean;
  messagesTabActive: boolean;

  unreadDialogCount: number;
  messageReadFromChat: Subscription;
  messageReadFromMiniChat: Subscription;

  constructor(
    private route: ActivatedRoute,
    private userService: UserService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer,
    private modalService: SuiModalService,    
    private tokenHelper: TokenHelperService,
    public toastr: ToastsManager,
    private unreadDialogs: UnreadDialogsService,
    private chatEventsService: ChatEventsService,
    private menuEventsService: MenuEventsService) { }

  ngOnInit() {
    this.dataLoaded = true;
    this.route.params
      .switchMap((params: Params) => this.userService.getUser(params['id']))
      .subscribe(resp => {
        this.user = resp.body as User;
        this.user.Birthday = new Date(this.user.Birthday);
        this.backgroundUrl = this.buildSafeUrl(this.user.Background != null ? this.user.Background : "https://www.beautycolorcode.com/d8d8d8.png");

        this.unreadDialogs.getUnreadDialogsCount(this.user.AccountId).then(x=> this.unreadDialogCount = x).catch(err=>console.log(err));
      });

    switch (this.route.snapshot.queryParams['tab']) {
      case 'tasks': this.tasksTabActive = true; break;
      case 'messages': this.messagesTabActive = true; break;
    }

    this.messageReadFromChat = this.chatEventsService.readMessageFromMiniChatToChatEvent$.subscribe(x=> {--this.unreadDialogCount});
    this.messageReadFromMiniChat = this.chatEventsService.readMessageFromChatToMiniChatEvent$.subscribe(x => --this.unreadDialogCount);
  }

  ngOnDestroy(){
   this.messageReadFromChat.unsubscribe();
   this.messageReadFromMiniChat.unsubscribe(); 
  }

  buildSafeUrl(link: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustStyle(`url('${link}')`);
  }

  bannerListener($event) {
    let file: File = $event.target.files[0];
    this.uploading = true;
    this.photoService.uploadToImgur(file)
    .then(link => {
      console.log(link);
      return this.photoService.saveBanner(link);
    })
    .then(link => {
      this.backgroundUrl = this.buildSafeUrl(link);
      this.uploading = false;
      this.toastr.success('Your background was updated', 'Success!');
    })
    .catch(err => {
      console.log(err);
      this.uploading = false;
      this.toastr.error('Sorry, something went wrong', 'Error!');
    });

  }

  selectAvatar(): void {
    this.modalService.open(new ImageCropperModal())
      .onApprove(result => {
        this.photoService.saveAvatar(result as string)
          .then(resp => this.user.Avatar = resp);
        this.photoService.saveCroppedAvatar(result as string)
          .then(resp => this.user.CroppedAvatar = resp);
        this.menuEventsService.croppedAvatar(result as string);
      });
  }
}