import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser'

import 'rxjs/add/operator/switchMap';

import { Vendor } from '../../models/vendor.model';

import { VendorService } from "../../services/vendor.service";
import { PhotoService } from '../../services/photo.service';

@Component({
  selector: 'app-vendor-edit',
  templateUrl: './vendor-edit.component.html',
  styleUrls: ['./vendor-edit.component.sass']
})
export class VendorEditComponent implements OnInit {
  vendor: Vendor;

  uploading: boolean;
  backgroundUrl: SafeResourceUrl

  constructor(
    private route: ActivatedRoute,
    private vendorService: VendorService,
    private photoService: PhotoService,
    private sanitizer: DomSanitizer
  ) { }

  ngOnInit() {
    this.route.params
      .switchMap((params: Params) => this.vendorService.getVendor(params['id']))
      .subscribe(resp => {
        this.vendor = resp.body as Vendor;
        this.backgroundUrl = this.buildSafeUrl(this.vendor.Background);
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
      }).catch(err => {
        console.log(err);
        this.uploading = false;
      });
  }
}