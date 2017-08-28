import { MessageModel } from "./message.model";

export interface DialogModel {
    Id: number;
    FirstParticipant: string;
    //FirstParticipantsId: number;
    SecondParticipant: string;
    //SecondParticipantsId: number;
    Messages: MessageModel[];
}