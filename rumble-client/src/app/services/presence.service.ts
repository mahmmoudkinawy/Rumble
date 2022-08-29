import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { HubConnection } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class PresenceService {
  private hubConnection: HubConnection | null = null;

  constructor(private snackBar: MatSnackBar) {}

  createHubConnection(user:)
}
