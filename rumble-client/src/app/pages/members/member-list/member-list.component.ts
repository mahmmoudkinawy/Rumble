import { Component, OnInit } from '@angular/core';

import { Member } from 'src/app/models/member';
import { Pagination } from 'src/app/models/pagination';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss'],
})
export class MemberListComponent implements OnInit {
  members: Member[] | null = null;
  pagination: Pagination | null = null;
  pageNumber = 1;
  pageSize = 4;

  constructor(private membersService: MembersService) {}

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.membersService
      .getMembers(this.pageNumber, this.pageSize)
      .subscribe((response) => {
        this.members = response.result;
        this.pagination = response.pagination;
      });
  }
}
