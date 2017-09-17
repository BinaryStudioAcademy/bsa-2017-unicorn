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
import { MenuEventsService } from "../../services/events/menu-events.service";
import { Subscription } from "rxjs/Subscription";
import { UnreadDialogsService } from "../../services/chat/unread-dialogs.service";
import { ChatEventsService } from "../../services/events/chat-events.service";
import { ImageCropperModal } from '../../image-cropper-modal/image-cropper-modal.component';

@Component({
  selector: 'app-vendor-edit',
  templateUrl: './vendor-edit.component.html',
  styleUrls: ['./vendor-edit.component.sass']
})
export class VendorEditComponent implements OnInit {
  @ViewChild(VendorEditChartsComponent)
  charts: VendorEditChartsComponent;

  vendor: Vendor;

  uploading: boolean;
  backgroundUrl: SafeResourceUrl;

  messagesTabActive: boolean;
  worksTabActive: boolean;

  unreadDialogCount: number;
  messageReadFromChat: Subscription;
  messageReadFromMiniChat: Subscription;

  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService,
    private photoService: PhotoService,
    private modalService: SuiModalService,
    private sanitizer: DomSanitizer,
    private unreadDialogs: UnreadDialogsService,
    private chatEventsService: ChatEventsService,
    private menuEventsService: MenuEventsService
  ) {
  }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.vendorService.getVendor(params['id']))
      .subscribe(resp => {
        this.vendor = resp.body as Vendor;
        this.vendor.Birthday = new Date(this.vendor.Birthday);
        this.backgroundUrl = this.buildSafeUrl(this.vendor.Background != null ? this.vendor.Background : "https://www.beautycolorcode.com/d8d8d8.png");

        this.unreadDialogs.getUnreadDialogsCount(this.vendor.AccountId).then(x=> this.unreadDialogCount = x).catch(err=>console.log(err));
      });
      if (this.route.snapshot.queryParams['tab'] === 'messages') {
        this.messagesTabActive = true;
      }
      if (this.route.snapshot.queryParams['tab'] === 'works') {
        this.worksTabActive = true;
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

  selectAvatar(): void {
    this.modalService.open(new ImageCropperModal())
      .onApprove(result => {
        this.photoService.saveAvatar(result as string)
          .then(resp => this.vendor.Avatar = resp);
        this.photoService.saveCroppedAvatar(result as string)
          .then(resp => this.vendor.CroppedAvatar = resp);
        this.menuEventsService.croppedAvatar(result as string);
      });
  }

  activateCharts() {
    this.charts.enabled = true;
  }
}