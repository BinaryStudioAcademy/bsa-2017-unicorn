import { Component, OnInit } from '@angular/core';
import { PhotoService } from '../../services/photo.service';
import { Params, ActivatedRoute } from "@angular/router";
import { CompanyShort } from "../../models/company-page/company-short.model";
import { CompanyService } from "../../services/company-services/company.service";
import { MenuEventsService } from "../../services/events/menu-events.service";

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.sass']
})
export class CompanyEditComponent implements OnInit {
  menuEventsService: MenuEventsService;
  isDimmed: boolean = false;
  company: CompanyShort;  
  uploading: boolean;

  messagesTabActive: boolean;
  vendorsTabActive: boolean;
  
  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private photoService: PhotoService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyShort(params['id'])).subscribe(res => {
      this.company = res;
    }); 
    if (this.route.snapshot.queryParams['tab'] === 'messages') {
      this.messagesTabActive = true;
    }      
    if (this.route.snapshot.queryParams['tab'] === 'vendors') {
      this.vendorsTabActive = true;
    }   
  }

  bannerListener($event) {
    let file: File = $event.target.files[0];
    console.log(file);
    this.uploading = true;
    this.photoService.uploadToImgur(file).then(link => {      
      return this.photoService.saveAvatar(link);
    }).then(link => {      
        this.company.Avatar = link;
        this.menuEventsService.changedAvatar(link); 
        this.uploading = false;
      }).catch(err => {
        console.log(err);
        this.uploading = false;
        });
  }
}