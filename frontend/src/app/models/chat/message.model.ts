import { ChatFile } from "./chat-file";

export interface MessageModel{
    MessageId: number;
    DialogId: number;
    IsReaded: boolean;
    OwnerId: number;    
    Message: string;
    Files: ChatFile[];
    Date: Date; 
    isLoaded: boolean;
}