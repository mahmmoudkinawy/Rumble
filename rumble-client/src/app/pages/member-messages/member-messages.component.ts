import { Component, Input, OnInit } from '@angular/core';

import { Message } from 'src/app/models/message';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.scss'],
})
export class MemberMessagesComponent implements OnInit {
  @Input() username: string | null = null;
  messages: Message[] = [];

  constructor(private messageService: MessageService) {}

  ngOnInit(): void {
    this.getMessages();
  }

  getMessages() {
    this.messageService
      .getMessageThread(this.username!)
      .subscribe((messages) => (this.messages = messages));
  }
}
