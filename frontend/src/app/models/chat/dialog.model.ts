import { MessageModel } from "./message.model";

export interface DialogModel {
    Id: number;
    ParticipantOneId: number;    
    ParticipantTwoId: number;
    ParticipantName: string;    
    ParticipantAvatar: string;
    Messages: MessageModel[];
    IsReadedLastMessage: boolean;
    LastMessageTime: Date;
}