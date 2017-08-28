import { Component, OnInit } from '@angular/core';
import { PhotoService } from '../../services/photo.service';
import { Params, ActivatedRoute } from "@angular/router";
import { CompanyShort } from "../../models/company-page/company-short.model";
import { CompanyService } from "../../services/company-services/company.service";

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.sass']
})
export class CompanyEditComponent implements OnInit {
  isDimmed: boolean = false;
  company: CompanyShort;  
  uploading: boolean;
  
  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private photoService: PhotoService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyShort(params['id'])).subscribe(res => {
      this.company = res;
    });        
  }

  bannerListener($event) {
    let file: File = $event.target.files[0];
    console.log(file);
    this.uploading = true;
    this.photoService.uploadToImgur(file).then(link => {      
      return this.photoService.saveAvatar(link);
    }).then(link => {      
        this.company.Avatar = link;
        this.uploading = false;
      }).catch(err => {
        console.log(err);
        this.uploading = false;
        });
  }
}