import { User } from './user';

export class UserParams {
  gender: string = '';
  minAge = 15;
  maxAge = 100;
  pageNumber = 1;
  pageSize = 10;
  orderBy = 'lastActive';

  constructor(user: User) {
    this.gender = user.gender === 'Male' ? 'Female' : 'Male';
  }
}
