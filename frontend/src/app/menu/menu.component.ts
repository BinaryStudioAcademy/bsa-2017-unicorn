import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { SuiModalService } from 'ng2-semantic-ui';
import { Subscription } from 'rxjs/Subscription';

import { MenuItem } from './menu-item/menu-item';
import { RegisterModal } from '../register/register-component/register.component';

import { AuthenticationEventService } from '../services/events/authenticationevent.service';
import { AuthenticationLoginService } from '../services/auth/authenticationlogin.service';
import { TokenHelperService } from '../services/helper/tokenhelper.service';
import { AccountService } from "../services/account.service";

import { ProfileShortInfo } from "../models/profile-short-info.model";
import { Notification, NotificationType } from "../models/notification.model";

import { RoleRouter } from "../helpers/rolerouter";
import { NotificationService } from "../services/notifications/notification.service";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.sass'],
  providers: []
})
export class MenuComponent implements OnInit {
  items: MenuItem[];
  isEnabled: boolean;
  isLogged: boolean;

  onLogIn: Subscription;
  onLogOut: Subscription;

  roleRouter: RoleRouter;

  profileInfo: ProfileShortInfo;
  profileUrl: string;

  showAccountDetails: boolean;
  isNotificationsShow: boolean = false
  isArchivedNotificationsShown: boolean = false;
  notifications: Notification[];
  archivedNotifications: Notification[];
  newNotifications: Notification[];

  newNotification: Notification;

  constructor(
    private router: Router,
    private modalService: SuiModalService,
    private authEventService: AuthenticationEventService,
    private authLoginService: AuthenticationLoginService,
    private tokenHelper: TokenHelperService,
    private accountService: AccountService,
    private notificationService: NotificationService) {
    this.isLogged = this.tokenHelper.isTokenValid() && this.tokenHelper.isTokenNotExpired();
  }

  ngOnInit() {
    this.roleRouter = new RoleRouter();
    if (this.isLogged) {
      this.accountService.getShortInfo(+this.tokenHelper.getClaimByName("accountid"))
        .then(resp => {
          if(resp !== undefined){
            this.profileInfo = resp.body as ProfileShortInfo;
          }
          else{
            this.initEmptyProfile();
            this.profileUrl = "";
          }
        });
      this.notificationService.connect(+this.tokenHelper.getClaimByName("accountid"))
        .then(() => this.notificationService
          .listen<Notification>("OnNotificationRecieved", notification => this.addNotification(notification)));
      this.accountService.getNotifications(+this.tokenHelper.getClaimByName("accountid"))
        .then(resp => {
          this.notifications = (resp.body as Notification[]);
          this.newNotifications = this.notifications.filter(n => !n.IsViewed);
          this.archivedNotifications = this.notifications.filter(n => n.IsViewed);
          this.sortNotificationsByTime();
        });
      this.setProfileRoute();
    }
    else {
      this.initEmptyProfile();
      this.profileUrl = "";
    
    }

    this.addMenuItems();
    this.isEnabled = true;

    this.onLogIn = this.authEventService.loginEvent$
      .subscribe(() => {
        this.isLogged = true;
        this.notificationService.connect(+this.tokenHelper.getClaimByName("accountid"))
        .then(() => this.notificationService
          .listen<Notification>("OnNotificationRecieved", notification => this.addNotification(notification)));
      this.accountService.getNotifications(+this.tokenHelper.getClaimByName("accountid"))
        .then(resp => {
          this.notifications = (resp.body as Notification[]);
          this.newNotifications = this.notifications.filter(n => !n.IsViewed);
          this.archivedNotifications = this.notifications.filter(n => n.IsViewed);
          this.sortNotificationsByTime();
        });
        this.accountService.getShortInfo(+this.tokenHelper.getClaimByName("accountid"))
          .then(resp => this.profileInfo = resp.body as ProfileShortInfo);
        this.setProfileRoute();
      });

    this.onLogOut = this.authEventService.logoutEvent$
      .subscribe(() => {
        this.notificationService.disconnect();
        this.isLogged = false;
        this.initEmptyProfile();
        this.showAccountDetails = false;
        this.profileUrl = "/search";
      });
  }

  initEmptyProfile(){
    this.profileInfo = {
      Avatar: "",
      Email: "",
      Name: "",
      Role: ""
    };
  }

  ngOnDestroy() {
    this.onLogIn.unsubscribe();
    this.onLogOut.unsubscribe();
  }

  openModal() {
    this.modalService.open(new RegisterModal());
  }

  addMenuItems() {
    this.items = [{
      name: 'Search',
      route: 'search'
    }, {
      name: 'Vendors',
      route: '#'
    }, {
      name: 'Sign in',
      route: 'register'
    }];
  }

  signOut() {
    this.authLoginService.signOut();
    this.router.navigate(['index']);
  }

  goToAccount() {
    this.showAccountDetails = false;
    this.router.navigateByUrl(this.profileUrl);
  }

  onShowDetails() {
    this.showAccountDetails = !this.showAccountDetails;
    this.isNotificationsShow = false;
  }

  setProfileRoute(): void {
    const roleId = +this.tokenHelper.getClaimByName("roleid");
    const profileId = this.tokenHelper.getClaimByName("profileid");

    switch (roleId) {
      case 2:
        this.profileUrl = `/user/${profileId}/edit`;
        break;
      case 3:
        this.profileUrl = `/vendor/${profileId}/edit`;
        break;
      case 4:
        this.profileUrl = `/company/${profileId}/edit`;
        break;
      default:
        this.profileUrl = "/search";
        break;
    }
  }

  haveNewNotifications(): boolean {
    return this.newNotifications && this.newNotifications.length != 0;
  }

  haveArchivedNotifications(): boolean {
    return this.archivedNotifications && this.archivedNotifications.length != 0;
  }

  showNotifications() {
    this.isNotificationsShow = !this.isNotificationsShow;
    this.isArchivedNotificationsShown = false;
    this.showAccountDetails = false;
    this.newNotification = undefined;
  }

  addNotification(notification: Notification): void {
    notification.Time = new Date(notification.Time);
    this.newNotification = notification;

    if (notification.Type = NotificationType.ChatNotification) {
      var chatNotification = this.newNotifications.find(n => n.Type === NotificationType.ChatNotification);
      
      while (chatNotification !== undefined) {
        this.archiveNotification(chatNotification);
        chatNotification = this.newNotifications.find(n => n.Type === NotificationType.ChatNotification);
      }
    }

    this.notifications.push(notification);
    this.newNotifications = this.notifications.filter(n => !n.IsViewed);
    this.sortNotificationsByTime();

    setTimeout(() => this.newNotification = undefined, 3000);
  }

  archiveNotification(notification: Notification){
    notification.IsViewed = true;
    this.newNotifications.splice(this.newNotifications.findIndex(n => n.Id === notification.Id), 1);
    this.archivedNotifications.push(notification);
    this.sortNotificationsByTime();
    this.accountService.updateNotification(+this.tokenHelper.getClaimByName("accountid"), notification);
  }

  toggleNotifications(): void {
    this.isArchivedNotificationsShown = !this.isArchivedNotificationsShown;
  }

  removeNotification(notification: Notification): void {
    this.notifications.splice(this.notifications.findIndex(n => n.Id === notification.Id), 1);
    this.archivedNotifications.splice(this.notifications.findIndex(n => n.Id === notification.Id), 1);
    this.accountService.removeNotification(+this.tokenHelper.getClaimByName("accountid"), notification);
  }

  sortNotificationsByTime(): void {
    this.notifications.forEach(n => n.Time = new Date(n.Time));
    
    this.notifications.sort((a, b) => b.Time.getTime() - a.Time.getTime());
    this.newNotifications.sort((a, b) => b.Time.getTime() - a.Time.getTime());
    this.archivedNotifications.sort((a, b) => b.Time.getTime() - a.Time.getTime());
  }

  isPerformer(): boolean {
    let roleId = +this.tokenHelper.getClaimByName("roleid");
    return roleId === 3 || roleId === 4;
  }

  notificationClick(notification: Notification): void {
    let role = this.tokenHelper.getRoleName();
    let id = +this.tokenHelper.getClaimByName('profileid');
    if (notification.Type === NotificationType.TaskNotification) {
      if (role === 'user') {
        this.router.navigate([`user/${id}/edit`], {
          queryParams: {
            tab: 'tasks'
          }
        });
      } else {
        this.router.navigate(['dashboard']);
      }
    } else {
      this.router.navigate([`${role}/${id}/edit`], {
        queryParams: {
          tab: 'messages'
        }
      });
    }
  }
}
