import { Component, OnInit, ViewChild } from '@angular/core';
import { PhotoService } from '../../services/photo.service';
import { Params, ActivatedRoute } from "@angular/router";
import { CompanyShort } from "../../models/company-page/company-short.model";
import { CompanyService } from "../../services/company-services/company.service";
import { MenuEventsService } from "../../services/events/menu-events.service";

import { Subscription } from "rxjs/Subscription";
import { UnreadDialogsService } from "../../services/chat/unread-dialogs.service";
import { ChatEventsService } from "../../services/events/chat-events.service";

import { CompanyChartsComponent } from './company-charts/company-charts.component';

@Component({
  selector: 'app-company-edit',
  templateUrl: './company-edit.component.html',
  styleUrls: ['./company-edit.component.sass']
})
export class CompanyEditComponent implements OnInit {

  @ViewChild(CompanyChartsComponent)
  charts: CompanyChartsComponent;
  isDimmed: boolean = false;
  company: CompanyShort;  
  uploading: boolean;

  messagesTabActive: boolean;
  vendorsTabActive: boolean;
  worksTabActive: boolean;

  unreadDialogCount: number;
  messageReadFromChat: Subscription;
  messageReadFromMiniChat: Subscription;
  
  constructor(private companyService: CompanyService,
    private route: ActivatedRoute,
    private unreadDialogs: UnreadDialogsService,
    private chatEventsService: ChatEventsService,
    private photoService: PhotoService,
    private menuEventsService: MenuEventsService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.companyService.getCompanyShort(params['id'])).subscribe(res => {
      this.company = res;

      this.unreadDialogs.getUnreadDialogsCount(this.company.AccountId).then(x=> this.unreadDialogCount = x).catch(err=>console.log(err));
    }); 
    if (this.route.snapshot.queryParams['tab'] === 'messages') {
      this.messagesTabActive = true;
    }      
    if (this.route.snapshot.queryParams['tab'] === 'vendors') {
      this.vendorsTabActive = true;
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

  bannerListener($event) {
    let file: File = $event.target.files[0];
    console.log(file);
    this.uploading = true;
    this.photoService.uploadToImgur(file).then(link => {          
      return this.photoService.saveAvatar(link);
    }).then(link => {  
        this.menuEventsService.changedAvatar(link);       
        this.company.Avatar = link;        
        this.uploading = false;
      }).catch(err => {
        console.log(err);
        this.uploading = false;
        });
  }

  activateCharts() {
    this.charts.enabled = true;
  }
}