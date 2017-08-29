import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

import { SignalR, IConnectionOptions } from "ng2-signalr";
import { SignalRModule, ISignalRConnection, BroadcastEventListener, ConnectionStatus, ConnectionTransports } from 'ng2-signalr';

@Injectable()
export class NotificationService {
	private apiUrl: string;
	private connection: ISignalRConnection;
	
	public isConnected: boolean = false;

  	constructor(private signalR: SignalR) 
  	{ 

	}
	  
	connect(accountId: number): Promise<any> {
		let options: IConnectionOptions = { 
			hubName: 'NotificationHub',
			url: environment.apiUrl,
			qs: { accountId: accountId },			
		};

		return this.signalR.connect(options)
			.then(c => {
				this.connection = c;
				this.isConnected = true;
			});
	}

	listen<TResult>(methodName: string, callback: (TResult) => any): Promise<any> {
		if (!this.isConnected) {
			console.log("No connection with server SignalR service.")
			return;
		}

		let onMethodInvoked$ = new BroadcastEventListener<TResult>(methodName);

		this.connection.listen(onMethodInvoked$);
		onMethodInvoked$.subscribe(callback);
	}

	invoke<TParameter>(methodName: string, parameter: TParameter): Promise<any> {
		if (!this.isConnected) {
			console.log("No connection with server SignalR service.")
			return;
		}

		return this.connection.invoke(methodName, parameter);
	}

	disconnect() {
		this.connection.stop();
	}
	




}
