import { MessageModel } from "./message.model";

export interface DialogModel {
    Id: number;
    ParticipantOneId: number;    
    ParticipantTwoId: number;
    ParticipantOneHided: boolean;    
    ParticipantTwoHided: boolean;
    ParticipantName: string;    
    ParticipantAvatar: string;
    ParticipantType: string;
    ParticipantProfileId: number;
    Messages: MessageModel[];
    IsReadedLastMessage: boolean;
    LastMessageTime: Date;
}