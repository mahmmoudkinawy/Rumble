import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';

import { environment } from 'src/environments/environment';

import { Member } from '../models/member';
import { PaginationResult } from '../models/pagination';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  private readonly baseUrl = environment.apiUrl;
  members: Member[] = [];
  paginatedResult: PaginationResult<Member[]> = new PaginationResult<
    Member[]
  >();

  constructor(private http: HttpClient) {}

  getMembers(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this.http
      .get<Member[]>(`${this.baseUrl}/users`, { observe: 'response', params })
      .pipe(
        map((response) => {
          this.paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            this.paginatedResult.pagination = JSON.parse(
              response.headers.get('Pagination') as any
            );
          }
          return this.paginatedResult;
        })
      );
  }

  getMember(username: string) {
    const member = this.members.find((m) => m.username === username);
    if (member !== undefined) return of<Member>(member);
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
}
