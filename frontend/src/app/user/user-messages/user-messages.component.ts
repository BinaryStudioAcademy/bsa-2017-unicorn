import { Component, OnInit } from '@angular/core';
import { NgIf } from '@angular/common';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'app-user-messages',
    templateUrl: './user-messages.component.html',
    styleUrls: ['./user-messages.component.sass']
})
export class UserMessagesComponent implements OnInit {

    constructor() {
        this.dialogs[0] = new Dialog();
        this.dialogs[1] = new Dialog();
        this.dialogs[0].id = 0;
        this.dialogs[1].id = 1;
        this.dialogs[0].userName = "Tom Tailor";
        this.dialogs[1].userName = "Dan Ivanov";
        var m11: Message = new Message();
        var m12: Message = new Message();
        var m21: Message = new Message();
        var m22: Message = new Message();

        m11.date = new Date(Date.now());
        m11.me = true;
        m11.messageText = 'Hello. I need some help';

        m12.date = new Date(Date.now());
        m12.me = false;
        m12.messageText = 'Hello. What is a problem?';

        m21.date = new Date(Date.now());
        m21.me = false;
        m21.messageText = 'What about payment?!!!';

        m22.date = new Date(Date.now());
        m22.me = true;
        m22.messageText = 'Oh, sorry, toworow will be';

        this.dialogs[0].messages = new Array<Message>();
        this.dialogs[1].messages = new Array<Message>();

        this.dialogs[0].messages[0] = m11;
        this.dialogs[0].messages[1] = m12;
        this.dialogs[1].messages[0] = m21;
        this.dialogs[1].messages[1] = m22;
    }
    dialogs: Dialog[] = new Array<Dialog>();
    selectedId: number = 0;
    ngOnInit() {
    }
    onSelect(curId: number) {
        this.selectedId = curId;
    }
}
export class Message {
    me: boolean;
    messageText: string;
    date: Date;
}
export class Dialog {
    id: number;
    userName: string;
    messages: Message[];
}