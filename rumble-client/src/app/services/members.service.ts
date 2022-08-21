import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of, take } from 'rxjs';

import { environment } from 'src/environments/environment';

import { Member } from '../models/member';
import { User } from '../models/user';
import { UserParams } from '../models/userParams';
import { AccountService } from './account.service';
import { getPaginatedResult, getPaginationHeaders } from './pagination-helper';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  members: Member[] = [];
  membersCache = new Map(); //key-value-paries
  user: User | null = null;
  userParams: UserParams | null = null;

  constructor(
    private http: HttpClient,
    private accountService: AccountService
  ) {
    this.accountService.currentUser$.pipe(take(1)).subscribe((user) => {
      this.user = user;
      this.userParams = new UserParams(user);
    });
  }

  getUserParams() {
    return this.userParams;
  }

  setUserParams(params: UserParams) {
    this.userParams = params;
  }

  resetUserParams() {
    this.userParams = new UserParams(this.user!);
    return this.userParams;
  }

  getMembers(userParams: UserParams) {
    let response = this.membersCache.get(Object.values(userParams).join('-'));
    if (response) return of(response);

    let params = getPaginationHeaders(
      userParams.pageNumber,
      userParams.pageSize
    );

    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return getPaginatedResult<Member[]>(
      `${environment.apiUrl}/users`,
      params,
      this.http
    ).pipe(
      map((response) => {
        this.membersCache.set(Object.values(userParams).join('-'), response);
        return response;
      })
    );
  }

  getMember(username: string) {
    const member = [...this.membersCache.values()]
      .reduce((arr, elem) => arr.concat(elem.result), [])
      .find((member: Member) => member.username === username);

    if (member) return of(member);

    return this.http.get<Member>(`${environment.apiUrl}/users/${username}`);
  }

  updateMember(member: Member) {
    return this.http.put(`${environment.apiUrl}/users`, member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.http.put(
      `${environment.apiUrl}/users/set-main-photo/${photoId}`,
      {}
    );
  }

  deletePhoto(photoId: number) {
    return this.http.delete(
      `${environment.apiUrl}/users/delete-photo/${photoId}`
    );
  }

  addLike(username: string) {
    return this.http.post(`${environment.apiUrl}/likes/${username}`, {});
  }

  getLikes(predicate: string, pageNumber: number, pageSize: number) {
    let params = getPaginationHeaders(pageNumber, pageSize);

    params = params.append('predicate', predicate);

    return getPaginatedResult<Partial<Member[]>>(
      `${environment.apiUrl}/likes`,
      params,
      this.http
    );
  }
}
