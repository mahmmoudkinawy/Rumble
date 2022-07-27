import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';

import { Member } from '../models/member';

@Injectable({
  providedIn: 'root',
})
export class MembersService {
  members: Member[] = [];
  private readonly baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getMembers() {
    if (this.members.length > 0) return of<Member[]>(this.members);
    return this.http.get<Member[]>(`${this.baseUrl}/users`).pipe(
      map((members) => {
        this.members = members;
        return members;
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
}
