import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { DialogModel } from "../../models/chat/dialog.model";

@Injectable()
export class MenuEventsService {
  private _onAvatarCropped = new Subject<string>();  
  private _onAvatarChanged = new Subject<string>();    

  avatarCroppedEvent$ = this._onAvatarCropped.asObservable();    
  avatarChangedEvent$ = this._onAvatarChanged.asObservable();    

  croppedAvatar(avatar: string) {
    this._onAvatarCropped.next(avatar);
  }

  changedAvatar(avatar: string) {
    this._onAvatarChanged.next(avatar);
  }
}