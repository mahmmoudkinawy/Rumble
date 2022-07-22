import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss'],
})
export class NavbarComponent implements OnInit {
  model: any = {};
  loggedIn = false;

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {}

  login() {
    this.accountService.login(this.model).subscribe(() => {
      this.router.navigateByUrl('/members');
      this.loggedIn = true;
    });
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
    this.loggedIn = false;
  }
}
