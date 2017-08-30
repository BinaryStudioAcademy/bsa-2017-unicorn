import { MessageModel } from "./message.model";

export interface DialogModel {
    Id: number;
    ParticipantOneId: number;
    //FirstParticipantsId: number;
    ParticipantTwoId: number;
    ParticipantName: string;
    //SecondParticipantsId: number;
    Messages: MessageModel[];
}