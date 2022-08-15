import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';

import { environment } from 'src/environments/environment';

import { Member } from '../models/member';
import { PaginationResult } from '../models/pagination';
import { UserParams } from '../models/userParams';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private readonly baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) {}

  getMembers(userParams: UserParams) {
    let params = this.getPaginationHeaders(
      userParams.pageNumber,
      userParams.pageSize
    );

    params = params.append('minAge', userParams.minAge);
    params = params.append('maxAge', userParams.maxAge);
    params = params.append('gender', userParams.gender);
    params = params.append('orderBy', userParams.orderBy);

    return this.getPaginatedResult<Member[]>(`${this.baseUrl}/users`, params);
  }

  private getPaginatedResult<T>(url: string, params: HttpParams) {
    const paginatedResult: PaginationResult<T> = new PaginationResult<T>();

    return this.http.get<T>(url, { observe: 'response', params }).pipe(
      map((response) => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(
            response.headers.get('Pagination') as string
          );
        }
        return paginatedResult;
      })
    );
  }

  getMember(username: string) {
    return this.http.get<Member>(`${this.baseUrl}/users/${username}`);
  }

  updateMember(member: Member) {
    return this.http.put(`${this.baseUrl}/users`, member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }

  setMainPhoto(photoId: number) {
    return this.http.put(`${this.baseUrl}/users/set-main-photo/${photoId}`, {});
  }

  deletePhoto(photoId: number) {
    return this.http.delete(`${this.baseUrl}/users/delete-photo/${photoId}`);
  }

  private getPaginationHeaders(pageNumber: number, pageSize: number) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber); //error
    params = params.append('pageSize', pageSize); //error

    return params;
  }
}
