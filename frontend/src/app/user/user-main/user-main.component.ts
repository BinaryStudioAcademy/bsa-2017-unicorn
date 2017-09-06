import { Component, OnInit, Input, OnDestroy, ViewChild  } from '@angular/core';
import { UserService } from "../../services/user.service";
import { ActivatedRoute, Params, ParamMap } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import {SuiModule} from 'ng2-semantic-ui';
import { FormsModule }   from '@angular/forms';
import {User} from '../../models/user';
@Component({
  selector: 'app-user-main',
  templateUrl: './user-main.component.html',
  styleUrls: ['./user-main.component.sass']
})
export class UserMainComponent implements OnInit {

  user:User;
  backgroundUrl: SafeResourceUrl;
  constructor(  private route: ActivatedRoute,private sanitizer: DomSanitizer, 
    private userService: UserService) { }

  ngOnInit() {
    this.route.params
    .switchMap((params: Params) => this.userService.getUser(params['id']))
    .subscribe(resp => {
      this.user = resp.body as User;
      this.backgroundUrl = this.buildSafeUrl(this.user.Background != null ? this.user.Background : "https://www.beautycolorcode.com/d8d8d8.png");
    });
  }
  buildSafeUrl(link: string): SafeResourceUrl {
    return this.sanitizer.bypassSecurityTrustStyle(`url('${link}')`);
  }
  getImage() : string {
        return this.user.CroppedAvatar ? this.user.CroppedAvatar : this.user.Avatar ? this.user.Avatar : ''; 
      }
      
    
}
