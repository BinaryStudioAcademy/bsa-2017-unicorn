export class Notification {
	Id: number;
	Type: NotificationType;
	SourceItemId: number;
	Title: string;
	Description: string;
	Time: Date;
}

export enum NotificationType {
	TaskNotification,
	ChatNotification
}