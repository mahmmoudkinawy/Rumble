import { Component, Input, OnInit } from '@angular/core';

import { MatSnackBar } from '@angular/material/snack-bar';

import { Member } from 'src/app/models/member';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss'],
})
export class MemberCardComponent implements OnInit {
  @Input() member: Member | null = null;

  constructor(
    private memberService: MembersService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {}

  addLike(member: Member) {
    this.memberService.addLike(member.username).subscribe(() => {
      this.snackBar.open(`You liked ${member.knownAs}`, 'Close');
    });
  }
}
