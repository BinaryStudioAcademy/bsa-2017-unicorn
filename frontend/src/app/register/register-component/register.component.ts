import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { Location } from '@angular/common';

import { AuthenticationLoginService } from '../../services/auth/authenticationlogin.service';
import { AuthenticationEventService } from '../../services/events/authenticationevent.service';

import * as firebase from 'firebase/app';

import { HelperService } from '../../services/helper/helper.service';

import { RegisterService } from '../../services/register.service';
import { NguiMapModule, NgMapApiLoader, NgMapAsyncApiLoader, NguiMap } from "@ngui/map";
import { ComponentModalConfig, ModalSize, SuiModal } from 'ng2-semantic-ui';
import { LocationService } from "../../services/location.service";
import { LocationModel } from "../../models/location.model";
import { JwtHelper } from '../../helpers/jwthelper';
import { TokenHelperService } from '../../services/helper/tokenhelper.service';
import { ChatService } from '../../services/chat/chat.service';
import { ChatEventsService } from '../../services/events/chat-events.service';
import { AccountService } from '../../services/account.service';
import { ProfileShortInfo } from '../../models/profile-short-info.model';

export class RegisterModal extends ComponentModalConfig<void> {
  constructor() {
    super(RegisterComponent);
    this.size = "small";
    this.isInverted = true;
  }
}

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  providers: [RegisterService, NgMapAsyncApiLoader, JwtHelper]
})
export class RegisterComponent implements OnInit, OnDestroy {
  private currentUser: firebase.User = null;
  mode: string;
  error: boolean = false;

  public roles: { [role: string]: boolean } = {};

  social: any;

  isLogged: boolean;

  roleSelected = false;
  location: LocationModel;
  isCustomer = false;
  isVendor = false;
  isCompany = false;

  floader: boolean;
  gloader: boolean;
  tloader: boolean;

  isBanned: boolean;
  private adminAccountId: number = 16;

  constructor(
    public modal: SuiModal<void>,
    private zone: NgZone,
    private helperService: HelperService,
    public registerService: RegisterService,
    public authLoginService: AuthenticationLoginService,
    private authEventService: AuthenticationEventService,
    private locationService: LocationService,
    private apiLoader: NgMapAsyncApiLoader,
    private tokenHelper: TokenHelperService,
    private chatService: ChatService,
    private chatEventsService: ChatEventsService,
    private accountService: AccountService
  ) {

    this.mode = 'date';
    this.authLoginService.signOut();
    navigator.geolocation.getCurrentPosition(()=>{});
    this.isLogged = false;
    this.error = false;
    
    this.initRoles();
  }

  isLoading(): boolean {
    return this.floader || this.gloader || this.tloader;
  }

  handleErrorLogin() {
    this.zone.run(() => this.error = true);
  }

  handleResponse(resp: any): void {
    switch (resp.status) {
      case 204: {
        this.error = false;
        this.zone.run(() => this.isLogged = !this.isLogged);
        break;
      }
      case 200: {
        this.authLoginService.saveJwt(resp.headers.get('token'));
        this.isBanned = this.tokenHelper.isAccountBanned();

        if (!this.isBanned) {
          this.modal.deny(null);
          this.authEventService.signIn();
          this.zone.run(() => this.helperService.redirectAfterAuthentication());
        }
        else {
          this.floader = false;
          this.gloader = false;
          this.tloader = false;

          this.zone.run(() => this.isLoading());
        }

        this.error = false;
        break;
      }
      default: {
        this.handleErrorLogin();
        break;
      }
    }
  }
  

  loginWithGoogle() {
    this.gloader = true;
    this.authLoginService.loginWithGoogle()
      .then(resp => {
        this.gloader = false;
        this.handleResponse(resp);
      })
      .catch(err => {
        this.gloader = false;
        this.handleErrorLogin();
      });
  }

  loginWithFacebook() {
    this.floader = true;
    this.authLoginService.loginWithFacebook()
      .then(resp => {
        this.floader = false;
        this.handleResponse(resp);
      })
      .catch(err => {
        this.floader = false;
        this.handleErrorLogin();
      });
  }

  loginWithTwitter() {
    this.tloader = true;
    this.authLoginService.loginWithTwitter()
      .then(resp => {
        this.tloader = false;
        this.handleResponse(resp);
      })
      .catch(err => {
        this.tloader = false;
        this.handleErrorLogin();
      });
  }

  clearRoles() {
    for (let key in this.roles) {
      this.roles[key] = false;
    }
  }

  createChat(){    

    let ownerId = +this.tokenHelper.getClaimByName('accountid');
    
    this.accountService.getShortInfo(this.adminAccountId)
      .then(resp => {
        let admin = resp.body as ProfileShortInfo;

        this.chatService.findDialog(ownerId, this.adminAccountId)
          .then(resp => {
            let dialog = resp !== null ? resp : {
              Id: null,
              ParticipantOneId: ownerId,
              ParticipantTwoId: this.adminAccountId,
              ParticipantName: admin.Name,
              ParticipantAvatar: admin.Avatar,
              ParticipantProfileId: null,
              ParticipantType: "admin",
              Messages: null,
              LastMessageTime: null,
              IsReadedLastMessage: null,
              Participant1_Hided: false,
              Participant2_Hided: false
            };

            this.chatEventsService.openChat(dialog);
            this.modal.deny(null);
          });
      });
  }

  initRoles() {
    this.roles['customer'] = false;
    this.roles['vendor'] = false;
    this.roles['company'] = false;
  }

  ngOnInit() {
    this.location = this.locationService.getCurrentLocation();
    
    this.authLoginService.authState.subscribe(user => {
      if (user) {
        this.currentUser = user;
        
      } else {
        this.currentUser = null;
      }
    });
    
  }

  ngOnDestroy() {
    this.error = false;
    this.isLogged = false;
    this.isCompany = false;
    this.isVendor = false;
    this.isCustomer = false;
    this.roleSelected = false;
    this.social = undefined;
    this.clearRoles();
  }

  selectRole(role: string) {
    
    this.clearRoles();
    this.roles[role] = true;
  }
}