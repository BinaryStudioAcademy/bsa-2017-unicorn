import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

import { SignalR, IConnectionOptions } from "ng2-signalr";
import { SignalRModule, ISignalRConnection, BroadcastEventListener, ConnectionStatus } from 'ng2-signalr';

@Injectable()
export class NotificationService {
	private apiUrl: string;
	private connection: ISignalRConnection;
	
	public isConnected: boolean = false;

  	constructor(private signalR: SignalR) 
  	{ 

	}
	  
	connect(accountId: number) {
		let options: IConnectionOptions = { 
			hubName: 'NotificationHub',
			url: environment.apiUrl,
			qs: { accountId: accountId }
		};

		this.signalR.connect(options)
			.then(c => {
				this.connection = c;
				this.isConnected = true;
			});
	}

	listen<TResult>(methodName: string, callback: (TResult) => any): void {
		if (this.isConnected) {
			console.log("No connection with server SignalR service.")
			return;
		}

		let onMethodInvoked$ = new BroadcastEventListener<TResult>(methodName);
		
		this.connection.listen(onMethodInvoked$);
		onMethodInvoked$.subscribe(callback);
	}




}
