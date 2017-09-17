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
import { ModalService } from '../../../services/modal/modal.service';
import { ReportService } from '../../../services/report.service';
import { AccountService } from '../../../services/account.service';
import { ToastsManager, Toast } from 'ng2-toastr';

import { Report } from '../../../models/report/report.model';
import { ReportType } from '../../../models/report/reportType.model';
import { ProfileShortInfo } from '../../../models/profile-short-info.model';

@Component({
  selector: 'app-user-main-info',
  templateUrl: './user-main-info.component.html',
  styleUrls: ['./user-main-info.component.sass'],
  providers: [ModalService, ReportService]
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

  @ViewChild('modalTemplate')
  public modalTemplate: ModalTemplate<void, {}, void>;
  private activeModal: SuiActiveModal<void, {}, void>;

  isLogged: boolean;
  message: string;
  email: string;
  profileInfo: ProfileShortInfo;
  loader: boolean;

  constructor(private userService: UserService,
    private chatEventsService: ChatEventsService,
    private tokenHelper: TokenHelperService,
    private chatService: ChatService,
    private modalService: ModalService,
    private reportService: ReportService,
    private accountService: AccountService,
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

  openModal() {
    this.getAccount();
    this.message = undefined;
    this.activeModal = this.modalService.openModal(this.modalTemplate, ModalSize.Mini);
  }

  sendMessage(formData) {
    if (formData.valid) {
      this.loader = true;
      const report: Report = {
        Id: 1,
        Date: new Date(),
        Type: ReportType.complaint,
        Message: this.message,
        Email: this.email,
        ProfileId: this.user.Id,
        ProfileName: `${this.user.Name} ${this.user.SurName}`,
        ProfileType: 'customer'
      };

      this.reportService.createReport(report).then(resp => {
        this.loader = false;
        this.toastr.success('Thank you for your report!');
        this.activeModal.approve('approved');
      }).catch(err => {
        this.loader = false;
        this.toastr.error('Ooops! Try again');
      });
    }
  }

  getAccount() {
    this.isLogged = this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired();
    if (this.isLogged) {
      this.accountService.getShortInfo(+this.tokenHelper.getClaimByName('accountid'))
      .then(resp => {
        if (resp !== undefined) {
          this.profileInfo = resp.body as ProfileShortInfo;
          this.email = this.profileInfo.Email;
        }
      });
    } else {
      this.email = undefined;
    }
  }
}
