import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';

import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class PresenceService {
  private hubConnection: HubConnection | null = null;

  constructor(private snackBar: MatSnackBar) {}

  createHubConnection(user: User) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${environment.hubUrl}/presence`, {
        accessTokenFactory: () => user.token,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((error) => console.error(error));

    this.hubConnection.on('UserIsOnline', (username) =>
      this.snackBar.open(`${username} is connected.`)
    );

    this.hubConnection.on('UserIsOffline', (username) =>
      this.snackBar.open(`${username} is disconnected.`)
    );
  }

  stopHubConnection() {
    this.hubConnection?.stop().catch((error) => console.error(error));
  }
}
