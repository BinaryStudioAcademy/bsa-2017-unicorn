import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { SafeResourceUrl, DomSanitizer } from '@angular/platform-browser';
import { BookComponent } from '../../book/book/book.component';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';
import { Subscription } from 'rxjs/Subscription';

import 'rxjs/add/operator/switchMap';

import { Vendor } from '../../models/vendor.model';
import { VendorService } from '../../services/vendor.service';
import { TokenHelperService } from '../../services/helper/tokenhelper.service';

import { ImageCropperComponent, CropperSettings } from 'ng2-img-cropper';
import { PhotoService, Ng2ImgurUploader } from "../../services/photo.service";

import { FormsModule } from '@angular/forms';
import { Work } from "../../models/work.model";

@Component({
  selector: 'app-vendor-details',
  templateUrl: './vendor-details.component.html',
  styleUrls: ['./vendor-details.component.sass'],
  providers: [
    PhotoService,
    Ng2ImgurUploader
  ]
})
export class VendorDetailsComponent implements OnInit {

  @ViewChild('bookComponent') bookComponent: BookComponent;

  @ViewChild('cropper', undefined)
  cropper: ImageCropperComponent;
  enabled: boolean = false;
  enableTheme: boolean = false;
  saveImgButton: boolean = false;
  backgroundUrl: SafeResourceUrl;
  uploading: boolean;
  isOwner: boolean;
  dataLoaded: boolean;

  // Data for book component
  routePath: string;
  routeid: number;
  works: Work[];
  selectedWorkId: number;

  cropperSettings: CropperSettings;
  vendor: Vendor;
  isGuest: boolean;
  isUser: boolean;
  file: File;
  data: any;
  imageUploaded: boolean;

  onLogIn: Subscription;

  tabActive: boolean = false;
  constructor(
    private route: ActivatedRoute,
    private authEventService: AuthenticationEventService,
    private vendorService: VendorService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer,
    private tokenHelperService: TokenHelperService) {
    this.getCurrentRole();

    this.routePath = this.route.root.snapshot.firstChild.url[0].path;
    this.routeid = +this.route.snapshot.paramMap.get('id');
  }

  ngOnInit() {
    this.dataLoaded = true;
    this.route.params
      .switchMap((params: Params) => this.vendorService.getVendor(params['id']))
      .subscribe(resp => {
        this.vendor = resp.body as Vendor;
        this.backgroundUrl = this.buildSafeUrl(this.vendor.Background != null ? this.vendor.Background : "https://www.beautycolorcode.com/d8d8d8.png");
      });

    if (this.route.snapshot.queryParams['tab'] === 'reviews') {
      this.tabActive = true;
    }

    if (this.route.snapshot.queryParams['work']) {
      this.selectedWorkId = +this.route.snapshot.queryParams['work'];
    }

    this.onLogIn = this.authEventService.loginEvent$
      .subscribe(() => {
        this.isGuest = !this.isGuest;
        this.isUser = !this.isUser;
      });
  }

  ngOnDestroy() {
    this.onLogIn.unsubscribe();
  }

  getCurrentRole() {
    if (this.tokenHelperService.getToken() === null) {
      this.isGuest = true;
      this.isUser = false;
      return;
    }

    const userRoleId = +this.tokenHelperService.getClaimByName('roleid');
    this.isGuest = userRoleId === 1;
    this.isUser = userRoleId === 2;
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

  onWorksLoaded(works: Work[]) {
    this.works = works;
    if (this.isUser) {
      let work = this.works.find(x => x.Id === this.selectedWorkId);
      this.bookComponent.selectWork(work);
    }
  }

}
