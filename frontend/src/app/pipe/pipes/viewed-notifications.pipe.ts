import { Pipe, PipeTransform } from '@angular/core';

import { Notification } from "../../models/notification.model";

@Pipe({name: 'viewedNotifications'})
export class ViewedNotificationsPipe implements PipeTransform {
  transform(items: Notification[], showAll: boolean) : Notification[] {
    return items.filter(i => i.IsViewed === showAll);
  }
}
