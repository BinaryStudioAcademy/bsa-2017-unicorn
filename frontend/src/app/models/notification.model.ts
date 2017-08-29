export class Notification {
	Id: number;
	Type: NotificationType;
	SourceItemId: number;
	Message: string;
	Time: Date;
}

export enum NotificationType {
	TaskNotification,
	ChatNotification
}