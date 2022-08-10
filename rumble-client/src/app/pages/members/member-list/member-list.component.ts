import { Component, OnInit } from '@angular/core';
import { take } from 'rxjs';

import { Member } from 'src/app/models/member';
import { Pagination } from 'src/app/models/pagination';
import { User } from 'src/app/models/user';
import { UserParams } from 'src/app/models/userParams';
import { AccountService } from 'src/app/services/account.service';
import { MembersService } from 'src/app/services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss'],
})
export class MemberListComponent implements OnInit {
  members: Member[] | null = null;
  pagination: Pagination | null = null;
  userParams: UserParams | null = null;
  user: User | null = null;
  genderList = [
    { value: 'Male', displayName: 'Males' },
    { value: 'Female', displayName: 'Females' },
  ];

  constructor(
    private membersService: MembersService,
    private accountService: AccountService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.user = user;
      this.userParams = new UserParams(user);
    });
  }

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.membersService.getMembers(this.userParams!).subscribe((response) => {
      this.members = response.result;
      this.pagination = response.pagination;
    });
  }

  pageChanged(event: any) {
    this.userParams!.pageNumber = event.page;
    this.loadMembers();
  }

  resetFilters() {
    this.userParams = new UserParams(this.user!);
    this.loadMembers();
  }
}
