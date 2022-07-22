import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter<boolean>();
  model: any = {};

  constructor(private accountService: AccountService, private router: Router) {}

  ngOnInit(): void {}

  register() {
    this.accountService.register(this.model).subscribe(
      () => {
        this.router.navigateByUrl('/members');
        console.log('Return to login toaster');
      },
      (error) => {
        console.error(error);
      }
    );
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
