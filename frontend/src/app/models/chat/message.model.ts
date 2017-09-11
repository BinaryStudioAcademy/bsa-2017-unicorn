import { ChatFile } from "./chat-file";

export interface MessageModel{
    DialogId: number;
    IsReaded: boolean;
    OwnerId: number;    
    Message: string;
    Files: ChatFile[];
    Date: Date; 
    isLoaded: boolean;
}