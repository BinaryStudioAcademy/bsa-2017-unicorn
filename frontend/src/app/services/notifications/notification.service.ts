import { Injectable } from '@angular/core';

import { environment } from '../../../environments/environment';

import { SignalR, IConnectionOptions } from "ng2-signalr";
import { SignalRModule, ISignalRConnection, BroadcastEventListener, ConnectionStatus, ConnectionTransports } from 'ng2-signalr';

@Injectable()
export class NotificationService {
	private apiUrl: string;
	private connection: ISignalRConnection;
	private deferredListeners = [];

	public isConnected: boolean = false;

  	constructor(private signalR: SignalR) { }
	  
	connect(accountId: number): Promise<any> {
		let options: IConnectionOptions = { 
			hubName: 'NotificationHub',
			url: environment.apiUrl,
			qs: { accountId: accountId },	
			transport: [ ConnectionTransports.webSockets, ConnectionTransports.longPolling ]
		};

		return this.signalR.connect(options)
			.then(c => {
				this.connection = c;
				this.isConnected = true;
			})
			.then(() => {
				this.deferredListeners.forEach(l => {
					let listener = (l as DeferredListener);
					this.listen(listener.MethodName, listener.Callback);
				})
			});
	}

	listen<TResult>(methodName: string, callback: (TResult) => any): Promise<any> {
		if (this.connection === undefined) {
			console.log("No connection with server SignalR service. Waiting for connection.")
			let defferedListener = new DeferredListener();
			defferedListener.Callback = callback;
			defferedListener.MethodName = methodName;
			this.deferredListeners.push(defferedListener)
			return;
		}

		let onMethodInvoked$ = new BroadcastEventListener<TResult>(methodName);		

		this.connection.listen(onMethodInvoked$);
		onMethodInvoked$.subscribe(callback);
	}

	invoke<TParameter>(methodName: string, parameter: TParameter): Promise<any> {
		if (this.connection === undefined) {
			console.log("No connection with server SignalR service. Waiting for connection.")
			return;
		}
		return this.connection.invoke(methodName, parameter)
			.then(() => console.log(`${methodName} invoked on server side with parameter:`, parameter));
	}

	disconnect() {
		if (this.connection !== undefined)
			this.connection.stop();
	}
}

export class DeferredListener{
	MethodName: string;
	Callback: (any) => any;
}
