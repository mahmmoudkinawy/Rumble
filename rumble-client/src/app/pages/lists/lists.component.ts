import { Component, OnInit } from '@angular/core';

import { Member } from 'src/app/models/member';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.scss'],
})
export class ListsComponent implements OnInit {
  members: Partial<Member[]> | null = null;
  predicate = 'liked';

  constructor(private memberService: MembersService) {}

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.memberService.getLikes(this.predicate).subscribe((response) => {
      this.members = response;
    });
  }
}
