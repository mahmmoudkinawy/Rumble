import { Component, OnInit } from '@angular/core';

import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  model: any = {};
  loggedIn = false;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe((response) => {
      console.log(response);
      this.loggedIn = true;
    });
  }

  logout() {
    this.loggedIn = false;
    this.accountService.logout();
  }
}
