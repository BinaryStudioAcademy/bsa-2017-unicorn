import { Component, OnInit } from '@angular/core';
import { Company } from "../../models/company.model";
import { CompanyService } from "../../services/company.service";
import { PhotoService } from '../../services/photo.service';
import { Params, ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.sass']
})
export class CompanyEditComponent implements OnInit {
  isDimmed: boolean = false;
  company: Company;  

  uploading: boolean;
  
  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private photoService: PhotoService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompany(params['id'])).subscribe(res => {
      this.company = res;
      });        
  }

  bannerListener($event) {
      let file: File = $event.target.files[0];
      this.uploading = true;
      this.photoService.uploadToImgur(file).then(link => {
        console.log(link);
        return this.photoService.saveAvatar(link);
      }).then(link => {
        //this.backgroundUrl = this.buildSafeUrl(link);
        this.company.Avatar = link;
        this.uploading = false;
      }).catch(err => {
        console.log(err);
        this.uploading = false;
      });
  }

}
