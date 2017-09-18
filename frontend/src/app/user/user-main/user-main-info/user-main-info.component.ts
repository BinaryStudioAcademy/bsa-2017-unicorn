import { Component, OnInit, Input,ViewChild, AfterViewChecked } from '@angular/core';
import { NgModel, NgForm } from '@angular/forms';
import {SuiModule} from 'ng2-semantic-ui';
import { User } from '../../../models/user';
import { NguiMapModule, Marker } from "@ngui/map";
import { UserService } from "../../../services/user.service";
import { LocationService } from "../../../services/location.service";
import { Review} from "../../../models/review.model"
import { MapModel } from "../../../models/map.model";
import { DialogModel } from "../../../models/chat/dialog.model";
import { ChatEventsService } from "../../../services/events/chat-events.service";
import { TokenHelperService } from "../../../services/helper/tokenhelper.service";
import { ChatService } from "../../../services/chat/chat.service";

import { SuiModalService, TemplateModalConfig
  , ModalTemplate, ModalSize, SuiActiveModal } from 'ng2-semantic-ui';
import { ReportService } from '../../../services/report.service';
import { AccountService } from '../../../services/account.service';
import { ToastsManager, Toast } from 'ng2-toastr';

import { Report } from '../../../models/report/report.model';
import { ReportType } from '../../../models/report/reportType.model';
import { ProfileShortInfo } from '../../../models/profile-short-info.model';
import { FeedbackModal } from '../../../feedback-modal/feedback-modal.component';

@Component({
  selector: 'app-user-main-info',
  templateUrl: './user-main-info.component.html',
  styleUrls: ['./user-main-info.component.sass'],
  providers: [ReportService]
})
export class UserMainInfoComponent implements OnInit {
  @Input() user: User;
  rating: number;
  reviewsCount: number;
  map: MapModel;
  openChat: boolean = false;
  ownerId: number;
  dialog: DialogModel;
  isLoaded: boolean = false;
  isGuest: boolean;

  constructor(private userService: UserService,
    private chatEventsService: ChatEventsService,
    private chatService: ChatService,
    private modalService: SuiModalService,
    private tokenHelper: TokenHelperService,
    private toastr: ToastsManager) {}
 
    ngOnInit() {
     this.userService.getRating(this.user.Id)
     .then(resp =>{ this.rating = resp.body as number;
      this.map = {
        center: {lat: this.user.Location.Latitude, lng: this.user.Location.Longitude},
        zoom: 18,    
        title: this.user.Name,
        label: this.user.Name,
        markerPos: {lat: this.user.Location.Latitude, lng: this.user.Location.Longitude}    
      };  
      });

     this.ownerId = +this.tokenHelper.getClaimByName('accountid');
     this.isGuest = this.ownerId === 0;
          
     this.userService.getReviews(this.user.Id)
     .then(resp => this.reviewsCount = (resp.body as Review[]).length)
  }

  createChat(){
    this.isLoaded = true;
    if(this.ownerId === undefined){
      this.ownerId = +this.tokenHelper.getClaimByName('accountid');
    }
    this.chatService.findDialog(this.ownerId, this.user.AccountId).then(res => {
      if(res !== null){        
        this.dialog = res; 
        this.dialog.ParticipantName = this.user.Name + " " + this.user.SurName;       
        this.isLoaded = false;
        this.openChat = true;                      
      } 
      else{     
        this.dialog = {
          Id: null,
          ParticipantOneId: this.ownerId,
          ParticipantTwoId: this.user.AccountId,
          ParticipantName: this.user.Name + " " + this.user.SurName,
          ParticipantAvatar: null,
          ParticipantType: 'user',
          ParticipantProfileId: this.user.Id,
          Messages: null,
          LastMessageTime: null,
          IsReadedLastMessage: null,
          Participant1_Hided: false,
          Participant2_Hided: false
        };    
        this.openChat = true;
        this.isLoaded = false;
      }
      this.chatEventsService.openChat(this.dialog);     
    });
  }

  openReportModal(): void {
    this.modalService.open(new FeedbackModal("Report this user", ReportType.complaint, this.user.Id, `${this.user.Name} ${this.user.SurName}`, "customer"));
  }
}
